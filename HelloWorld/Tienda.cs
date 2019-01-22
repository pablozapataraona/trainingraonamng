using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelloWorld
{
    public class Store
    {
        public string Description;
        public DateTime OpenDate = DateTime.Today;
        public string Provincia;
        public string Responsable;
        public TipoTienda Tipo;
        public string TipoDeContenido;
        private int _ID;
        private ListItem AssociatedListItem;
        public Store()
        {
        }

        public Store(int ID)
        {
            this._ID = ID;
        }

        public Store(ListItem item)
        {
            ParseListItemToStore(item);
        }

        public enum TipoTienda { Indefinido, Propias, Franquicias, Deposito };

        public int ID => _ID;
        public void DeleteFromSharePoint()
        {
            if (this.AssociatedListItem == null) throw new Exception("El elemento no está asociado");
            this.AssociatedListItem.Recycle();
            this.AssociatedListItem.Context.ExecuteQuery();
            this.AssociatedListItem = null;
        }

        public void GetById(int id, ClientContext ctx)
        {
            var rootSite = ctx.Site.RootWeb;
            var list = rootSite.Lists.GetByTitle(Constants.Lists.Tiendas);

            var item = list.GetItemById(id);
            ctx.Load(item);
            ctx.Load(item, x => x.ContentType);
            ctx.ExecuteQueryWithIncrementalRetry();

            ParseListItemToStore(item);
        }

        public void SaveInSharePoint(ClientContext ctx)
        {
            var rootSite = ctx.Site.RootWeb;
            var list = rootSite.Lists.GetByTitle(Constants.Lists.Tiendas);

            var item = list.AddItem(new ListItemCreationInformation());
            ctx.ExecuteQuery();

            UpdateListItemProperties(ctx, rootSite, list, item);

            this._ID = item.Id;
        }

        public void SetStoreID(int id)
        {
            //Lógica de validación en BBDD para ver si existe o no....
            this._ID = id;
        }

        public void Update(ClientContext ctx)
        {
            if (this.ID == 0) throw new Exception("El elemento no ha sido inicializado");

            var rootSite = ctx.Site.RootWeb;
            var list = rootSite.Lists.GetByTitle(Constants.Lists.Tiendas);

            UpdateListItemProperties(ctx, rootSite, list, this.AssociatedListItem);
        }

        /// <summary>
        /// Convierte el objeto de SharePoint a objeto de entidad Store
        /// </summary>
        /// <param name="item"></param>
        private void ParseListItemToStore(ListItem item)
        {
            this.AssociatedListItem = item;
            this._ID = item.Id;
            this.Description = item[Constants.Columns.Titulo] != null ? item[Constants.Columns.Titulo].ToString() : string.Empty;
            this.OpenDate = item[Constants.Columns.FechaApertura] != null ? (DateTime)item[Constants.Columns.FechaApertura] : DateTime.MinValue;
            this.Responsable = item[Constants.Columns.Responsable] != null ? ((FieldUserValue)item[Constants.Columns.Responsable]).Email : string.Empty;
            this.Provincia = item[Constants.Columns.Pais] != null ? SharePoint.GetLabelFromTaxonomyObject(item[Constants.Columns.Pais]) : string.Empty;
            this.Tipo = item[Constants.Columns.TipoTienda] != null ? Helper.GetStoreEnumFromString(item[Constants.Columns.TipoTienda].ToString()) : Store.TipoTienda.Indefinido;
            this.TipoDeContenido = item.ContentType.Name;
        }
        private void UpdateListItemProperties(ClientContext ctx, Web rootSite, List list, ListItem item)
        {
            var user = rootSite.EnsureUser(this.Responsable);
            ctx.Load(user);
            ctx.ExecuteQueryWithIncrementalRetry();

            item[Constants.Columns.Titulo] = this.Description;
            item[Constants.Columns.TipoTienda] = this.Tipo.ToString();

            var responsable = new FieldUserValue()
            {
                LookupId = user.Id
            };
            item[Constants.Columns.Responsable] = responsable;
            item.Update();
            ctx.ExecuteQueryWithIncrementalRetry();

            if (string.IsNullOrEmpty(this.Provincia) == false)
            {
                UpdateTaxonomyField(ctx, list, item);
            }
        }
        private void UpdateTaxonomyField(ClientContext ctx, List list, ListItem item)
        {
            //Empezamos inicialización de término
            //Obtenemos el término asociado para obtener el id
            var taxonomySession = TaxonomySession.GetTaxonomySession(ctx);
            var store = taxonomySession.GetDefaultSiteCollectionTermStore();
            var group = store.GetSiteCollectionGroup(ctx.Site, false);
            ctx.Load(group);
            ctx.Load(group.TermSets);
            ctx.ExecuteQuery();

            //Aquí ha de apuntar a vuestro almacén de términos: Paises
            var termSet = group.TermSets.FirstOrDefault(x => x.Name == Constants.Taxonomy.Pais);
            LabelMatchInformation labelMatch = new LabelMatchInformation(ctx)
            {
                TermLabel = this.Provincia,
                Lcid = 1033,
                TrimUnavailable = true
            };

            var terms = termSet.GetTerms(labelMatch);
            ctx.Load(terms);
            ctx.ExecuteQuery();
            var taxonomyProvincia = terms.First(x => x.Name == this.Provincia);

            //Obtenemos el campo asociado en el listado para coger su ID
            var provinciaField = list.Fields.GetByInternalNameOrTitle(Constants.Columns.Pais);
            ctx.Load(provinciaField, x => x.Id);
            ctx.ExecuteQuery();

            //Seteamos el campo, hace falta tener instalado el PnP de SharePoint
            item.SetTaxonomyFieldValue(provinciaField.Id, this.Provincia, taxonomyProvincia.Id);
            item.SystemUpdate();
            ctx.ExecuteQuery();
        }
    }

    public class StoreCollection : List<Store>
    {
        public List<Store> GetByDate(DateTime date)
        {
            var items = this.Where(tienda => tienda.OpenDate == date).OrderByDescending(x => x.OpenDate).ToList();
            return items;
        }

        public void GetFromSharePoint(ClientContext ctx)
        {
            var rootSite = ctx.Site.RootWeb;
            var list = rootSite.Lists.GetByTitle(Constants.Lists.Tiendas);
            var items = list.GetItems(CamlQuery.CreateAllItemsQuery());
            ctx.Load(items);
            ctx.Load(items, col => col.Include(item => item.ContentType, item => item.Id));
            ctx.ExecuteQuery();

            this.Clear();

            foreach (var item in items)
            {
                this.Add(new Store(item));
            }
        }
    }
}
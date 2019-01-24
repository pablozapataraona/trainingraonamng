using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoTraining.Business.Provision
{
    public class Library
    {
        public string Name { get; set; }
        public bool EnableVersioning { get; set; }

        public bool EnableContentTypes { get; set; }

        public ListTemplateType ListTemplate { get; set; }

        public List AssociatedList { get; set; }

        public ContentTypes ContentTypes { get; set; }

        public void Create(ClientContext ctx)
        {

            Console.WriteLine("Creating list {0}", this.Name);
            this.AssociatedList = ctx.Web.CreateList(this.ListTemplate, this.Name, this.EnableVersioning, true, enableContentTypes: this.EnableContentTypes);

            if (this.EnableContentTypes)
            {
                if (this.ContentTypes != null)
                {
                    foreach (var ct in this.ContentTypes)
                    {
                        this.AssociatedList.AddContentTypeToListByName(ct.Name);
                    }
                }
            }

        }

        public void Delete (ClientContext ctx)
        {
            var library = ctx.Web.GetListByTitle(this.Name);
            library.DeleteObject();
            ctx.ExecuteQuery();

        }
    }

    public class Libraries : List<Library>
    {
        public void Create(ClientContext ctx)
        {
            foreach (var library in this)
            {
                try
                {
                    Console.WriteLine("Creating library {0}", library.Name);
                    library.Create(ctx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Creating library {0} failed due to: {1}", library.Name, ex.Message);
                }
            }
        }

        public void Delete(ClientContext ctx)
        {
            foreach (var library in this)
            {
                try
                {
                    Console.WriteLine("Deleting library {0}", library.Name);
                    library.Delete(ctx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deleting library {0} failed due to: {1}", library.Name, ex.Message);
                }
            }
        }
    }
}

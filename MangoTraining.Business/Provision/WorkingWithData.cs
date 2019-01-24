using Microsoft.SharePoint.Client;
using System;

namespace MangoTraining.Business.Provision
{
    public class WorkingWithData
    {
        public class WebAndSubsites
        {
            public static void GetSiteCollectionProperties(ClientContext ctx)
            {
                var site = ctx.Site;
                ctx.Load(site);
                ctx.ExecuteQuery();

                Console.WriteLine("Site collection name URL {0}", site.Url);
            }

            public static void GetRootWebProperties(ClientContext ctx)
            {
                var rootWeb = ctx.Site.RootWeb;
                ctx.Load(rootWeb);
                ctx.ExecuteQuery();

                Console.WriteLine("Root site title is {0}", rootWeb.Title);
            }

            public static void GetSubsiteByUrl(ClientContext ctx, string url)
            {
                var subweb = ctx.Site.OpenWeb(url);
                ctx.Load(subweb); ;
                ctx.ExecuteQuery();
            }


        }

        public class ListItems
        {
            /// <summary>
            /// Get all Items
            /// </summary>
            /// <param name="ctx"></param>
            /// <param name="libraryTitle"></param>
            public static void GetAllItemsInList(ClientContext ctx, string libraryTitle)
            {
                var list = ctx.Web.Lists.GetByTitle(libraryTitle);
                var items = list.GetItems(CamlQuery.CreateAllItemsQuery());
                ctx.Load(items);
                ctx.ExecuteQuery();

                foreach (var item in items)
                {
                    Console.WriteLine("Item id is {0} and title is {1}", item.Id, item["Title"]);
                }
            }

            /// <summary>
            /// Get Items based query
            /// </summary>
            /// <param name="ctx"></param>
            /// <param name="libraryTitle"></param>
            public static void GetAllItemsWithQuery(ClientContext ctx, string libraryTitle)
            {
                var list = ctx.Web.Lists.GetByTitle(libraryTitle);
                var camlQuery = new CamlQuery()
                {
                    ViewXml = ""
                };

                var items = list.GetItems(camlQuery);
                ctx.Load(items);
                ctx.ExecuteQuery();

                foreach (var item in items)
                {
                    Console.WriteLine("Item id is {0} and title is {1}", item.Id, item["Title"]);
                }
            }


            /// <summary>
            /// Get Item by ID
            /// </summary>
            /// <param name="ctx"></param>
            /// <param name="libraryTitle"></param>
            /// <param name="itemID"></param>
            public static void GetItemById(ClientContext ctx, string libraryTitle, int itemID)
            {
                var list = ctx.Web.Lists.GetByTitle(libraryTitle);
                var item = list.GetItemById(itemID);
                ctx.Load(item);
                ctx.ExecuteQuery();

                Console.WriteLine("Item id is {0} and title is {1}", item.Id, item["Title"]);
            }

            /// <summary>
            /// Get Item by ID
            /// </summary>
            /// <param name="ctx"></param>
            /// <param name="libraryTitle"></param>
            /// <param name="itemID"></param>
            /// <param name="fieldsToRetrieve">If defined, retrieves only the selected fields</param>
            public static void GetItemByIdWithSpecificFields(ClientContext ctx, string libraryTitle, int itemID)
            {
                var list = ctx.Web.Lists.GetByTitle(libraryTitle);
                var item = list.GetItemById(itemID);
                ctx.Load(item, x => x["Title"], x => x["Author"]);
                ctx.ExecuteQuery();

                Console.WriteLine("Item id is {0} and title is {1}", item.Id, item["Title"]);
            }

            /// <summary>
            /// Get items Paged
            /// </summary>
            /// <param name="ctx"></param>
            /// <param name="camlQuery"></param>
            public static void GetItemsPaged(ClientContext ctx, string libraryTitle, CamlQuery camlQuery)
            {
                Web oWebsite = ctx.Web;

                ListItemCollectionPosition position = null;
                List list = oWebsite.Lists.GetByTitle(libraryTitle);
                var itemPageLimit = 10;

                string query = $@"<View><OrderBy>
                                            <FieldRef Name='Id' />
                                        </OrderBy>
                                        <Query/>
                                 <RowLimit>" + itemPageLimit + @"</RowLimit></View>";


                do
                {

                    var items = list.GetItems(camlQuery);
                    ctx.Load(items);
                    ctx.ExecuteQueryWithIncrementalRetry();
                    position = items.ListItemCollectionPosition;

                    foreach (ListItem item in items)
                    {
                        Console.WriteLine("Item id is {0} and title is {1}", item.Id, item["Title"]);
                    }
                }
                while (position != null);
            }

            //Create Item

            //Update Item

            //Delete Item
        }

        public class ContentTypes
        {
        }

        public class Pages
        {

        }

        public class Documents
        {

        }

        public class Taxonomy
        {

        }

        public class Search
        {

        }
    }
}

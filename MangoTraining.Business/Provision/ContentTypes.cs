using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoTraining.Business.Provision
{
    public class ContentType
    {
        public string Name { get; set; }
        public string ContentTypeId { get; set; }

        public string Group { get; set; }

        public string Description { get; set; }

        public Microsoft.SharePoint.Client.ContentType Parent { get; set; }

        public List<Column> ColumnsToAdd { get; set; }

        public Microsoft.SharePoint.Client.ContentType AssociatedContentType { get; set; }

        public void Create(ClientContext ctx)
        {
            Console.WriteLine("Creating content type {0}", this.Name);
            var createdCT = ctx.Web.CreateContentType(this.Name, this.Description, this.ContentTypeId.ToString(), this.Group, this.Parent);
            this.AssociatedContentType = createdCT;

            foreach (var column in this.ColumnsToAdd)
            {
                createdCT.AddFieldByName(column.FieldInformation.InternalName);
            }

        }

        public void Delete(ClientContext ctx)
        {
            Console.WriteLine("Deleting content type {0}", this.Name);
            ctx.Web.DeleteContentTypeByName(this.Name);
        }
    }

    public class ContentTypes : List<ContentType>
    {
        public void Create(ClientContext ctx)
        {

            foreach (var ct in this)
            {
                try
                {
                    ct.Create(ctx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while creating content type {0}:  {1}", ct.Name, ex.Message);
                }
            }
        }

        public void Delete(ClientContext ctx)
        {

            foreach (var ct in this)
            {
                try
                {
                    ct.Delete(ctx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while deleting content type {0}:  {1}", ct.Name, ex.Message);
                }
            }
        }

    }
}

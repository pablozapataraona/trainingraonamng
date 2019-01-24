using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeDevPnP.Core;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Entities;

namespace MangoTraining.Business.Provision
{

    public class Column
    {
        public FieldCreationInformation FieldInformation { get; set; }

        public Field AssociatedField { get; set; }


        public void Create(ClientContext ctx, bool execute)
        {

            if (FieldInformation == null) throw new Exception("Field Information Cannot be null");
            OfficeDevPnP.Core.Entities.FieldCreationInformation fld = new FieldCreationInformation(FieldInformation.FieldType)
            {
                AdditionalAttributes = this.FieldInformation.AdditionalAttributes,
                AddToDefaultView = this.FieldInformation.AddToDefaultView,
                ClientSideComponentId = this.FieldInformation.ClientSideComponentId,
                ClientSideComponentProperties = this.FieldInformation.ClientSideComponentProperties,
                DisplayName = this.FieldInformation.DisplayName,
                Group = this.FieldInformation.Group,
                Id = this.FieldInformation.Id,
                InternalName = this.FieldInformation.InternalName,
                Required = this.FieldInformation.Required
            };

            this.AssociatedField = ctx.Web.CreateField(fld, execute);
        }

        public void Delete(ClientContext ctx)
        {
            var field = ctx.Web.GetFieldByInternalName(this.FieldInformation.InternalName);
            field.DeleteObject();
            ctx.ExecuteQuery();
        }
    }

    public class Columns : List<Column>
    {
        public void Create(ClientContext ctx, bool execute)
        {
            foreach (var column in this)
            {
                try
                {
                    Console.WriteLine("Creating column {0}", column.FieldInformation.InternalName);
                    column.Create(ctx, execute);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Creating column {0} failed due to: {1}", column.FieldInformation.InternalName, ex.Message);
                }
            }
        }

        public void Delete(ClientContext ctx)
        {
            foreach (var column in this)
            {
                try
                {
                    Console.WriteLine("Deleting column {0}", column.FieldInformation.InternalName);
                    column.Delete(ctx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deleting column {0} failed due to: {1}", column.FieldInformation.InternalName, ex.Message);
                }
            }
        }
    }
}

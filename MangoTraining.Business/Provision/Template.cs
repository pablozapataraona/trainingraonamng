using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoTraining.Business.Provision
{
    public class Template
    {
        public Columns Columns { get; set; }
        public ContentTypes ContentTypes { get; set; }

        public Libraries Libraries { get; set; }

        public void ApplyTemplate(ClientContext ctx)
        {
            try
            {
                if (this.Columns != null)
                {

                    this.Columns.Create(ctx, false);

                }

                ctx.ExecuteQuery();

                if (this.ContentTypes != null)
                {
                    try
                    {
                        this.ContentTypes.Create(ctx);
                    }
                    catch
                    {

                    }
                }

                if (this.Libraries != null)
                {
                    try
                    {
                        this.Libraries.Create(ctx);
                    }
                    catch
                    {

                    }
                }


            }
            catch
            {
                throw;
            }

        }

        public void RemoveEntities(ClientContext ctx)
        {
            try
            {
                if (this.Libraries != null)
                {
                    try
                    {
                        this.Libraries.Delete(ctx);
                    }
                    catch
                    {

                    }
                }

                if (this.ContentTypes != null)
                {
                    try
                    {
                        this.ContentTypes.Delete(ctx);
                    }
                    catch
                    {

                    }
                }

                if (this.Columns != null)
                {

                    this.Columns.Delete(ctx);

                }


            }
            catch
            {
                throw;
            }

        }
    }
}

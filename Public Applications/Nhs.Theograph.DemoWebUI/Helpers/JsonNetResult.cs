namespace Nhs.Theograph.DemoWebUI.Helpers
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using Newtonsoft.Json;

    public class JsonNetResult : ActionResult
    {
        public JsonNetResult()
        {
            this.SerializerSettings = new JsonSerializerSettings();
        }

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public object Data { get; set; }

        public JsonSerializerSettings SerializerSettings { get; set; }

        public Formatting Formatting { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType)
              ? ContentType
              : "application/json";

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting };
                JsonSerializer serializer = JsonSerializer.Create(this.SerializerSettings);
                serializer.Serialize(writer, Data);
                writer.Flush();
            }
        }
    }
}
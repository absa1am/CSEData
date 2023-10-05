using HtmlAgilityPack;

namespace CSEData.Worker
{
    public class WebScrabber
    {
        private readonly string _htmlUrl;

        public WebScrabber(string htmlUrl)
        {
            _htmlUrl = htmlUrl;
        }

        public HtmlDocument GetHtmlDocument()
        {
            var web = new HtmlWeb();

            var htmlDoc = web.Load(_htmlUrl);

            return htmlDoc;
        }

        public IList<object> GetData()
        {
            var nodes = GetHtmlDocument().DocumentNode.SelectNodes("//tbody/tr/td");

            int n = nodes.Count;
            IList<object> rows = new List<object>();

            for (var i = 0; i < n; i++)
            {
                if (nodes[i].NodeType == HtmlNodeType.Element)
                {
                    var row = new
                    {
                        SL = nodes[i++].InnerText.ToString(),
                        CompanyCode = nodes[i++].InnerText.ToString(),
                        PriceLTP = nodes[i++].InnerText.ToString(),
                        Open = nodes[i++].InnerText.ToString(),
                        High = nodes[i++].InnerText.ToString(),
                        Low = nodes[i++].InnerText.ToString(),
                        YCP = nodes[i++].InnerText.ToString(),
                        Trade = nodes[i++].InnerText.ToString(),
                        Value = nodes[i++].InnerText.ToString(),
                        Volume = nodes[i].InnerText.ToString(),
                        Time = DateTime.Now
                    };

                    rows.Add(row);
                }
            }

            return rows;
        }

        public bool IsMarketOpen()
        {
            var node = GetHtmlDocument().DocumentNode.SelectSingleNode("//div[@class='market_status']/div/span");

            return node.InnerText.Contains("Open");
        }
    }
}

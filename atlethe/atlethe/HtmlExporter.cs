using System.IO;
using System.Linq;
using System.Text;

namespace AthleteCatalog
{
    static class HtmlExporter
    {
        public static void ExportAll(ItemRepository repo, string templateFolder, string outputFolder)
        {
            string template = File.ReadAllText(Path.Combine(templateFolder, "template.html"));

            ExportIndex(repo, template, outputFolder);
            ExportItems(repo, template, outputFolder);
            ExportFavorites(repo, template, outputFolder);

            File.Copy(Path.Combine(templateFolder, "style.css"),
                      Path.Combine(outputFolder, "style.css"), true);
        }

        static void ExportIndex(ItemRepository repo, string template, string output)
        {
            string content = template;

            content = content.Replace("{{TITLE}}", "Athlete Catalog – Főoldal");
            content = content.Replace("{{DESCRIPTION}}", "Sportolók adatbázisa, statisztikákkal és kedvencekkel.");

            content = content.Replace("{{ITEMS}}",
                $"<p>Sportolók száma: {repo.Items.Count}</p>" +
                $"<p>Kategóriák száma: {repo.Items.Select(i => i.Category).Distinct().Count()}</p>");

            File.WriteAllText(Path.Combine(output, "index.html"), content);
        }

        static void ExportItems(ItemRepository repo, string template, string output)
        {
            string content = template;

            content = content.Replace("{{TITLE}}", "Összes sportoló");
            content = content.Replace("{{DESCRIPTION}}", "Minden sportoló listája táblázatban.");

            StringBuilder sb = new StringBuilder();
            sb.Append("<table><tr><th>Név</th><th>Sportág</th><th>Év</th><th>Rating</th></tr>");

            foreach (var i in repo.Items)
            {
                sb.Append($"<tr><td>{i.Name}</td><td>{i.Category}</td><td>{i.Year}</td><td>{i.Rating}</td></tr>");
            }

            sb.Append("</table>");

            content = content.Replace("{{ITEMS}}", sb.ToString());

            File.WriteAllText(Path.Combine(output, "items.html"), content);
        }

        static void ExportFavorites(ItemRepository repo, string template, string output)
        {
            string content = template;

            content = content.Replace("{{TITLE}}", "Kedvenc sportolók");
            content = content.Replace("{{DESCRIPTION}}", "Csak a kedvencek kártyák formájában.");

            var favs = repo.Items.Where(i => i.IsFavorite).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var i in favs)
            {
                sb.Append($@"
                <div class='card'>
                    <h3>{i.Name}</h3>
                    <p><b>Sportág:</b> {i.Category}</p>
                    <p>{i.Description}</p>
                    <p><b>Rating:</b> {i.Rating}</p>
                </div>");
            }

            content = content.Replace("{{ITEMS}}", sb.ToString());

            File.WriteAllText(Path.Combine(output, "favorites.html"), content);
        }
    }
}

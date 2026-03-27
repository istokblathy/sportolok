
const toggle = document.getElementById("darkToggle");

toggle?.addEventListener("click", () => {
    document.body.classList.toggle("light");
});


function searchTable() {
    const input = document.getElementById("search").value.toLowerCase();
    const rows = document.querySelectorAll("tbody tr");

    rows.forEach(row => {
        const text = row.innerText.toLowerCase();
        row.style.display = text.includes(input) ? "" : "none";
    });
}


function filterSport() {
    const selected = document.getElementById("filter").value;
    const rows = document.querySelectorAll("tbody tr");

    rows.forEach(row => {
        const sport = row.dataset.sport;

        if (selected === "all" || sport === selected) {
            row.style.display = "";
        } else {
            row.style.display = "none";
        }
    });
}
const baseUrl = "https://bechdel.azurewebsites.net/api/";
const pageSize = 12;
async function getAll(page, year) {
  let response;
  if (year) {
    response = await fetch(getUrl(`films/${year}/?page=${page}&pageSize=${pageSize}`));
  } else {
    response = await fetch(getUrl(`films/?page=${page}&pageSize=${pageSize}`));
  }
  return await response.json();
}
async function getPassed(page, year) {
  let response;
  if (year) {
    response = await fetch(getUrl(`films/passed/${year}/?page=${page}&pageSize=${pageSize}`));
  } else {
    response = await fetch(getUrl(`films/passed/?page=${page}&pageSize=${pageSize}`));
  }
  return await response.json();
}
async function getFailed(page, year) {
  let response;
  if (year) {
    response = await fetch(getUrl(`films/failed/${year}/?page=${page}&pageSize=${pageSize}`));
  } else {
    response = await fetch(getUrl(`films/failed/?page=${page}&pageSize=${pageSize}`));
  }
  return await response.json();
}
async function getYears(page, year) {
  const response = await fetch(getUrl(`years`));
  return await response.json();
}
function getUrl(path) {
  if (path[0] === "/") throw error("Path must start without the /");
  return `${baseUrl}${path}`;
}
var api = {
  getAll,
  getPassed,
  getFailed,
  getYears
};

const resultsPane = document.getElementById("results");
const yearSelect = document.getElementById("year-select");
const prevPage = document.getElementById("prev-page");
const nextPage = document.getElementById("next-page");

const Categories = {
  None: Symbol("NONE"),
  All: Symbol("ALL"),
  Passed: Symbol("PASSED"),
  Failed: Symbol("FAILED"),
};

const status = {
  category: Categories.None,
  page: 1,
  pages: 0,
  total: 0,
  currentYear: null
};

yearSelect.addEventListener("change", async (e) => {
  await getResults();
});

prevPage.addEventListener("click", async (e) => {
  status.page--;
  await getResults();
});

nextPage.addEventListener("click", async (e) => {
  status.page++;
  await getResults();
});

function renderFilms(response) {
  resultsPane.innerHTML = "";
  const filmDivs = [];
  for (let film of response.results) {
    filmDivs.push(formatFilm(film));
  }
  resultsPane.innerHTML = filmDivs.join("");
}

async function fillSelect() {

  // Fill the year dropdown on first call (Always All and all years)
  if (yearSelect.options.length === 0) {
    const years = await api.getYears();
    for (year of ["All Years", ...years]) {
      yearSelect.add(new Option(year, year == "All Years" ? null : Number(year)));
    }
  }
}

async function getResults(category) {
  await fillSelect();
  let response;

  // default to last used
  if (!category) category = status.category;

  // If it's changed, update the status.
  const selectedYear = yearSelect.value == "null" ? null : Number(yearSelect.value);
  if (status.category != category || selectedYear != status.currentYear) {
    if (category) status.category = category;
    status.page = 1;
    status.currentYear = selectedYear;
  }


  // Load the data
  if (category === Categories.All) {
    response = await api.getAll(status.page, status.currentYear);
  } else if (category === Categories.Failed) {
    response = await api.getFailed(status.page, status.currentYear);
  } else if (category === Categories.Passed) {
    response = await api.getPassed(status.page, status.currentYear);
  } else {
    console.error("Bad Category Used...");
  }

  status.pageCount = response.pageCount;
  status.count = response.count;

  renderFilms(response);
  enablePaging();
}

function enablePaging() {
  if (status.page == 1) prevPage.classList.add("hidden");
  else prevPage.classList.remove("hidden");
  if (status.page >= status.pageCount) nextPage.classList.add("hidden");
  else nextPage.classList.remove("hidden");
}

function formatFilm(film) {
  return `<div class="film border rounded-lg  bg-gray-300/50 p-2 border-black">
    <img src="${film.posterUrl}" alt="${film.title}" />
    <div class="title">${film.title}</div>
    <div class="info">${film.year}</div>
    <div class="info">${film.rating}</div>
    <div class="info">Passed: ${film.passed}</div>
    <div class="info">Reason: ${film.reason}</div>
    <div class="info">Budget: $${film.budget.toLocaleString("en-US")}</div>
    <div class="info">Domestic Gross: $${film.domesticGross.toLocaleString(
      "en-US"
    )}</div>
    <div class="info">International Gross: $${film.internationalGross.toLocaleString(
      "en-US"
    )}</div>
    <p>${film.overview}</p>
  </div>`;
}

const loadAll = async () => await getResults(Categories.All);
const loadPassed = async () => await getResults(Categories.Passed);
const loadFailed = async () => await getResults(Categories.Failed);

const allMenu = document.getElementById("menu-all");
const passMenu = document.getElementById("menu-pass");
const failMenu = document.getElementById("menu-fail");

function initMenu () {
  allMenu.addEventListener("click", async () => {
    await loadAll();
    updateMenu(Categories.All);
  });
  passMenu.addEventListener("click", async () => {
    await loadPassed();
    updateMenu(Categories.Passed);
  });
  failMenu.addEventListener("click", async () => {
    await loadFailed();
    updateMenu(Categories.Failed);
  });
}

function updateMenu(category) {
  const process = (menuItem, selected) => {
    if (!selected) menuItem.classList.remove("selected");
    else {
      if (!menuItem.classList.contains("selected")) menuItem.classList.add("selected");
    }
  };

  process(allMenu, category === Categories.All);
  process(passMenu, category === Categories.Passed);
  process(failMenu, category === Categories.Failed);
}

document.addEventListener("DOMContentLoaded", async (e) => {
  initMenu();
  await loadAll();
});
//# sourceMappingURL=main.js.map

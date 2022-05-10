import api from "./api.js";

const resultsPane = document.getElementById("results");
const yearSelect = document.getElementById("year-select");
const prevPage = document.getElementById("prev-page");
const nextPage = document.getElementById("next-page");

export const Categories = {
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
  if (status.page == 1) prevPage.classList.add("hidden")
  else prevPage.classList.remove("hidden")
  if (status.page >= status.pageCount) nextPage.classList.add("hidden")
  else nextPage.classList.remove("hidden")
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

export const loadAll = async () => await getResults(Categories.All);
export const loadPassed = async () => await getResults(Categories.Passed);
export const loadFailed = async () => await getResults(Categories.Failed);

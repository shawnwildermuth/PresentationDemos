import { loadAll, loadFailed, loadPassed, Categories } from "./results";

const allMenu = document.getElementById("menu-all");
const passMenu = document.getElementById("menu-pass");
const failMenu = document.getElementById("menu-fail");

export default function () {
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

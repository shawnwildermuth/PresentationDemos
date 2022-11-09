import { loadAll } from "./results.js";
import initMenu from "./menu";

document.addEventListener("DOMContentLoaded", async (e) => {
  initMenu();
  await loadAll();
});

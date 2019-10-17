import Vue from "vue";
import Vuex from "vuex";
import api from "./services/api";

Vue.use(Vuex);

export default new Vuex.Store({
  strict: true, // may want to disable for production
  state: {
    isBusy: false,
    error: "",
    regions: [],
    currentSites: [],
    cart: []
  },
  mutations: {
    setError(state, error) { state.error = error; },
    setBusy(state, busy) { state.isBusy = busy; },
    setRegions(state, regions) { state.regions = regions; },
    setCurrentSites(state, sites) { state.currentSites = sites; },
    addToCart(state, item) { 
      if (!state.cart.find(i => i === item)) state.cart.push(item); 
    },
    clearCart(state) { state.cart = []; }
  },
  actions: {
    async loadRegions({ commit }) {
      var regions = await api.loadRegions();
      commit("setRegions", regions);
    },
    async loadSites({ state, commit }, region) {
      if (region) {
        let sites = await api.loadSites(region);
        commit("setCurrentSites", sites);
        return;
      }
      commit("setError", "Failed to load sites");
    }
  }
});
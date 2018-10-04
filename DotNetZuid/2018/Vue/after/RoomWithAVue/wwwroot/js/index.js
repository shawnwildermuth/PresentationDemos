// index.js
Vue.filter("date", function (val) {
  return val.toDateString();
});


let app = new Vue({
  el: "#theApp",
  data: {
    name: "Shawn in Eindhoven",
    eventDate: new Date(),
    rooms: [],
    isBusy: false
  },
  created: function () {
    this.isBusy = true;
    axios.get("/api/rooms")
      .then(r => {
        this.rooms = r.data;
      })
      .finally(r => this.isBusy = false);
  },
  methods: {
    addNew: function () {
      this.rooms.push({
        title: "New Room",
        description: "This is a room",
        rate: 99.99,
        maxGuests: 1,
        images: [
          { url: "/img/omni-1.jpg" }
        ]
      });
  }
}

});
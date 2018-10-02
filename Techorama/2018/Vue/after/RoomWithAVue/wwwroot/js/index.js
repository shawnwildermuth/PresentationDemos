Vue.filter("date", (val) => {
  return val.toDateString();
});

var app = new Vue({
  el: "#theApp",
  data: {
    name: "Techorama",
    eventDate: new Date(),
    isBusy: false,
    rooms: []
  },
  created: function () {
    this.isBusy = true;
    axios.get("api/rooms")
      .then((r) => {
        this.rooms = r.data;
      })
      .finally(()=> this.isBusy = false);
  },
  methods: {
    onNew() {
      this.rooms.push({
        title: "New Room",
        description: "This is the Omni Room",
        rate: 99.99,
        maxGuests: 1,
        images: [
          { url: "/img/omni-1.jpg" }
        ]
      });
    }
  }

});
// app.js
var vm = new Vue({
  el: "#app",
  data: {
    name: "Shawn",
    isBusy: false,
    rooms: [
      //{
      //  title: "Omni Room",
      //  description: "This is the Omni Room",
      //  rate: 149.99,
      //  maxGuests: 3,
      //  images: [
      //    { url: "/img/omni-1.jpg" }
      //  ]
      //},
      //{
      //  title: "Varsity Room",
      //  description: "This is the Varsity Room",
      //  rate: 129.99,
      //  maxGuests: 2,
      //  images: [
      //    { url: "/img/varsity-1.jpg" }
      //  ]
      //},
      //{
      //  title: "Calanwold",
      //  description: "This is the Calanwold Room",
      //  rate: 199.99,
      //  maxGuests: 3,
      //  images: [
      //    { url: "/img/omni-2.jpg" }
      //  ]
      //}
    ]

  },
  mounted: function () {
    this.isBusy = true;
    axios.get("/api/rooms")
      .then((r) => {
        this.rooms = r.data;
        this.isBusy = false;
      });
  },
  methods: {
    onBook: function (room) {
      alert(room.title);
    },
    onNew: function () {
      this.rooms.push({ title: "NEW ROOM", 
          description: "This is the Calanwold Room",
          rate: 199.99,
          maxGuests: 3,
          images: [
            { url: "/img/omni-2.jpg" }
          ]
        }
);
    }
  }
});
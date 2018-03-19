Vue.filter("shortDate", function (val) {
  return val.toDateString();
});

var vm = new Vue({
  el: "main",
  data: {
    name: "Hello Orlando",
    today: new Date(),
    color: "warning",
    isBusy: true,
    rooms: [
      {
        title: "Omni Room",
        description: "This is the Omni Room",
        rate: 149.99,
        maxGuests: 3,
        images: [
          { url: "/img/omni-1.jpg" }
        ]
      },
      {
        title: "Varsity Room",
        description: "This is the Varsity Room",
        rate: 129.99,
        maxGuests: 2,
        images: [
          { url: "/img/varsity-1.jpg" }
        ]
      }
    ]

  },
  methods: {
    onToggle: function () {
      this.isBusy = !this.isBusy;
    },
    onSort: function (by) {
      this.rooms = _.sortBy(this.rooms, by);
    }
  }
});
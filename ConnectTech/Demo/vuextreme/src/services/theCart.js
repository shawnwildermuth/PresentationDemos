let theCart = {
  cart: [],
  addToCart(item){
    if (!this.cart.includes(item)) this.cart.push(item);
  },
  clear() {
    this.cart = [];
  }
};

export default theCart;
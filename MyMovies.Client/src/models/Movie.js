export default class Movie {
  id = "";
  title = "";
  productionYear = null;

  constructor(data) {
    if (data) {
      this.id = data.id ? data.id : "";
      this.title = data.title ? data.title : "";
      this.productionYear = data.productionYear ? data.productionYear : "";
    }
  }
}

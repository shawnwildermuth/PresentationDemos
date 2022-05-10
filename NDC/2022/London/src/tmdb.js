const tmdbKey = "dd20be9e8c350f1f0b02f350348a5c6a";
const imageUrlStart = "https://image.tmdb.org/t/p/w200";
export default async function getFilmInfo(imdbid) {
  const url = `https://api.tmdb.org/find/${imdbid}?api_key=${tmdbKey}&external_source=imdb_id`;
  const result = await fetch(url);
  const film = await result.json();
  


}
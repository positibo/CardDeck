import axios from "axios";

class DeckService {
  resetCard = () => {
    const url = process.env.VUE_APP_HOST_URL + "/cardDeckV1/ResetCard";
    return axios.get(url);
  };

  shuffleCard = () => {
    const url = process.env.VUE_APP_HOST_URL + "/cardDeckV1/ShuffleCard";
    return axios.get(url);
  };

  takeCard = () => {
    const url = process.env.VUE_APP_HOST_URL + "/cardDeckV1/TakeCard";
    return axios.get(url);
  };

  takeCards = numberOfCards  => {
    const url =process.env.VUE_APP_HOST_URL + "/cardDeckV1/TakeCards/" + numberOfCards;
    return axios.get(url);
  };
  getAllCardRanks = () => {
    const url = process.env.VUE_APP_HOST_URL + "/cardDeckV1/GetAllCardRanks";
    return axios.get(url);
  };
  getAllSuits = () => {
    const url = process.env.VUE_APP_HOST_URL + "/cardDeckV1/GetAllSuits";
    return axios.get(url);
  };
}

const deckService = new DeckService();
export default deckService;

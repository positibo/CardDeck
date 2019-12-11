<template>
  <div class="container">
    <AlertComponent v-if="showAlert" v-bind="alertInfo"></AlertComponent>

    <h1 class="title">
      Welcome to Shuffle your Deck!
    </h1>
    <div>Number of Shuffles: {{ shuffleCount }}</div>
    <div class="main-buttons">
      <button @click="shuffleDeck" class="btn">Shuffle</button>
      <button v-if="isShuffled" @click="initializeDeck" class="btn">Reset</button>
      <button v-if="isShuffled" @click="takeCard" class="btn">Take Card</button>
      <input
        v-model="numberOfCards"
        type="text"
        v-if="isShuffled"
        class="input"
        required=""
      />
      <button v-if="isShuffled" @click="takeCards" class="btn">Take Cards</button>
    </div>
    <div class="deck">
      <div v-for="card in cards" :key="card.cardId" class="card">
        <div class="card_suit_top"> 
          <label >{{ card.suitName }}</label>
        </div>
        <div class="card_number"> 
          <label>{{ card.cardNumber }} </label>
        </div>
        <div class="card_suit_bottom"> 
          <label>{{ card.suitName }}</label>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import DeckService from "../components/deck.service";
import AlertComponent from "../shared/alert/alert.component";

export default {
  name: "DeckComponent",
  components: {
    AlertComponent
  },
  data() {
    return {
      ranks: [],
      suits: [],
      cards: [],
      isShuffled: false,
      numberOfCards: 0,
      shuffleCount: 0,
      showAlert: null,
      alertInfo: null
    };
  },
  created() {
    this.initializeDeck();
  },
  methods: {
    initializeDeck() {
      let vm = this;
      vm.cards = [];

      DeckService.resetCard()
        .then(response => {
          vm.cards = response.data;
          vm.showAlert = false;
        })
        .catch(function(error) {
          vm.alertInfo = { success: false, error };
          vm.showAlert = true;
        });

      vm.isShuffled = false;
      vm.shuffleCount = 0;
    },
    shuffleDeck() {
      let vm = this;
       if (vm.cards.length == 0) {
        let error = "Empty cards in your Deck.";
        vm.alertInfo = { success: false, message: error };
        vm.showAlert = true;
        return;
      }

      DeckService.shuffleCard()
        .then(response => {
          vm.cards = response.data;
          vm.showAlert = false;
        })
        .catch(function(error) {
          vm.alertInfo = { success: false, error };
          vm.showAlert = true;
        });

      this.isShuffled = true;
      this.shuffleCount = this.shuffleCount + 1;
    },
    takeCard() {
      let vm = this;
      if (vm.cards.length == 0) {
        let error = "Empty cards in your Deck.";
        vm.alertInfo = { success: false, message: error };
        vm.showAlert = true;
        return;
      }

      DeckService.takeCard()
        .then(response => {
          let card = response.data;
          vm.cards.splice(card, 1);
          vm.showAlert = false;
        })
        .catch(function(error) {
          vm.alertInfo = { success: false, error };
          vm.showAlert = true;
        });
    },
    takeCards() {
      let vm = this;
      if (vm.numberOfCards == 0) {
        let error = "Enter number of cards.";
        vm.alertInfo = { success: false, message: error };
        vm.showAlert = true;
        return;
      }

      if (vm.cards.length == 0) {
        let error = "Empty cards in your Deck.";
        vm.alertInfo = { success: false, message: error };
        vm.showAlert = true;
        return;
      }

      if (vm.numberOfCards > vm.cards.length) {
        let error = "Cannot take more than the number of existing cards.";
        vm.alertInfo = { success: false, message: error };
        vm.showAlert = true;
        return;
      }

      DeckService.takeCards(vm.numberOfCards)
        .then(response => {
          let cardItems = response.data;
          cardItems.forEach(card => {
            vm.cards.splice(card, 1);
            vm.showAlert = false;
          });
        })
        .catch(function(error) {
          vm.alertInfo = { success: false, error };
          vm.showAlert = true;
        });
    },
    getAllCardRanks(){
      let vm = this;
       DeckService.getAllCardRanks()
        .then(response => {
          vm.ranks = response.data;
          vm.showAlert = false;
        })
        .catch(function(error) {
          vm.alertInfo = { success: false, error };
          vm.showAlert = true;
        });
    },
    getAllSuits(){
      let vm = this;
       DeckService.getAllSuits()
        .then(response => {
          vm.suits = response.data;
          vm.showAlert = false;
        })
        .catch(function(error) {
          vm.alertInfo = { success: false, error };
          vm.showAlert = true;
        });
    },
  }
};
</script>

<style scoped>
@import url("https://fonts.googleapis.com/css?family=Roboto+Slab:300,400,700");

html,
body,
#app {
  height: 100%;
  background: ghostwhite;
}

.title {
  font-family: Roboto Slab, sans-serif;
  text-align: center;
  padding-top: 30px;
  margin-bottom: 0 !important;
  font-weight: 300;
  font-size: 3rem;
}

.main-buttons {
  display: block;
  margin: 0 auto;
  text-align: center;
  padding-top: 30px;
  margin-bottom: 0 !important;
}

.btn{
  margin: 10px;
  padding: 8px;
  font-size: 20px;
}

.input{
  margin: 10px;
  padding: 8px;
  font-size: 20px;
}

.deck {
  margin-left: 30px;
  padding-top: 30px;
}

.card {
  width: 110px;
  height: 150px;
  float: left;
  margin-right: 25px;
  margin-bottom: 25px;
  border-color:black;
  border-style: solid;
  border-width: 1px;
}

.card_suit_top {
  text-align: left;
  padding: 5px;
}

.card_suit_bottom {
  text-align: right;
  padding-right: 5px;
}

.card_number {
  width: 100%;
  padding-top: 35px;
  padding-bottom: 35px;
  text-align: center;
  font-weight: bold;
  font-size: 25px;
}

</style>

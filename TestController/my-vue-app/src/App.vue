<template>
<div>
<form @submit.prevent="SendData">
<label for="Name1">Имя пользователя 1:</label>
    <input type="text" name="Name1" v-model="user1">
    <br>
    <label for="Number1">Число пользователя 1:</label>
    <input type="number" name="Number1" v-model="num1">
    <br>
    <label for="Name2">Имя пользователя 2:</label>
    <input type="text" name="Name2" v-model="user2">
    <br>
    <label for="Number2">Число пользователя 2:</label>
    <input type="number" name="Number2" v-model="num2">
    <br><br>
    <button @click="SendData">Отправить</button>
</form>
  </div>
  <br><br>
  <div class="formMove">
    <form @submit.prevent="SendMove">
    <label for="row">Строка:</label>
    <input type="number" name="row" v-model="_row">
    <br>
    <label for="column">Столбец:</label>
    <input type="number" name="column" v-model="_col">
    <br><br>
    <button @click="SendMove" v-bind:disabled="isDisabled">Отправить</button>
</form>
  </div>
<h3> {{ gamer }}</h3>
<br>
<div class="all_div" id="all_div">
  <div class="setka" id="div0">
  <h1>{{ Board_Row1[0] }}</h1>
  </div>
  <div class="setka" id="div1">
    <h1>{{ Board_Row1[1] }}</h1>
  </div>
  <div class="setka" id="div2">
  <h1>{{ Board_Row1[2] }}</h1>
  </div>
  <div class="setka" id="div3">
  <h1>{{ Board_Row2[0] }}</h1>
  </div>
  <div class="setka" id="div4">
  <h1>{{ Board_Row2[1] }}</h1>
  </div>
  <div class="setka" id="div5">
  <h1>{{ Board_Row2[2] }}</h1>
  </div>
  <div class="setka" id="div6">
  <h1>{{ Board_Row3[0] }}</h1>
  </div>
  <div class="setka" id="div7">
  <h1>{{ Board_Row3[1] }}</h1>
  </div>
  <div class="setka" id="div8">
  <h1>{{ Board_Row3[2] }}</h1>
  </div>
</div>
<br>
<button @click="Restart">Начать игру заново</button>
</template>

<script setup lang="ts">
//import axios from "axios";
import { ref } from "vue";
import { SendUserData, Move } from "./func";


const user1 = ref('');
const user2 = ref('');
const num1 = ref(0);
const num2 = ref(0);
let priority = 0;
const _row = ref(1);
const _col = ref(1);
let Board_Row1 = ["", "", ""];
let Board_Row2 = ["", "", ""];
let Board_Row3 = ["", "", ""];
let gamer_start = "";
let gamer = "";
let message = "";
let isDisabled: boolean = false;
   
const SendData = async () => {
  const username1 = user1.value;
  const username2 = user2.value;
  const number1 = num1.value;
  const number2 = num2.value;
    try {
      //console.log("SendData");
        const response = await SendUserData(username1, number1, username2, number2);
          if (response.data != "Usernames must be different")
            {
              priority = response.data;
              if(priority == 0){
                gamer_start = gamer = "Сейчас ходят крестики";
              }
              else{
                gamer_start = gamer = "Сейчас ходят нолики"
              }
              return response.data;
            } 
            } catch (error) {

            }
          }

const SendMove = async () => {
  const row = _row.value;
  const column = _col.value;
  const User1 = user1.value;
  const User2 = user2.value;
    try {
      const response = await Move(row, column, User1, User2, Board_Row1, Board_Row2, Board_Row3, priority, message);
      if (response.data != "Incorrect position for the value" || response.data != "The cell is occupied"){
        Board_Row1 = response.data.board_Row1
        Board_Row2 = response.data.board_Row2
        Board_Row3 = response.data.board_Row3
        priority = response.data.priority
        if(priority == 0){
                gamer = "Сейчас ходят крестики";
              }
              else{
                gamer = "Сейчас ходят нолики"
              }
        if(response.data.message == "The player for the crosses won")
          gamer = "Игрок за крестики победил";
        if(response.data.message == "The player won by noughts")
          gamer = "Игрок за нолики победил";
        if(response.data.message == "Draw"){
          gamer = "Ничья";
        isDisabled = true;
        }
      }
    } catch (error) {

    }
}

const Restart = () => {
  Board_Row1 = ["", "", ""];
  Board_Row2 = ["", "", ""];
  Board_Row3 = ["", "", ""];
  isDisabled = false;
  gamer = gamer_start;
}

</script>

<style scoped>
.setka{
  width: 100px;
  height: 100px;
  border: 1px solid black;
  float: left;
}

button{
  background-color: aqua;
  color: #000;
}

.all_div{
  width: 305px;
  height: 305px;
  border: 1px solid black;
}

.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
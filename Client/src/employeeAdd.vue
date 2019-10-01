<template>
    <form @submit.prevent="AddEmployee">
        <ul class="list-group">
            <li class="list-group-item">
                <div class="row">
                    <label class="col-sm">Введите фамилию сотрудника</label>
                    <input  class="col-sm" required type="text"  name="surname" v-model="surname" placeholder="Иванов" >
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class="col-sm">Введите имя сотрудника</label>
                    <input class="col-sm" required type="text"  name="name" v-model="name" placeholder="Иван">
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class="col-sm">Введите отчество сотрудника</label>
                    <input class="col-sm" required type="text"  name="patronymic" v-model="patronymic" placeholder="Иванович"> 
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class="col-sm">Введите e-mail сотрудника</label>
                    <input class="col-sm" required type="email"  name="email" v-model="email" placeholder="ivanov@ivanov.com">
                </div> 
            </li>
            <input type="submit" class="btn btn-primary" value = "Добавить"/>
        </ul>
  </form>
</template>

<script>
import axios from 'axios'

export default {
    data(){
        return{
            email: '',
            surname: '',
            name: '',
            patronymic: '',
            employees:[]
        }
    },
    methods:{
        AddEmployee(){
            let options = {
            headers: { 'Content-Type': 'application/json' },
            url: 'https://localhost:44316/api/employee/create',
            method: 'post',
            data: JSON.stringify({
                surname: this.surname, 
                name: this.name,
                patronymic: this.patronymic,
                email: this.email
                })
            }
            axios(options)
                    .then(response => {
                        this.employees = response.data;
                        console.log(this.employees);
                        alert("Сотрудник добавлен");
                    })
                    .catch(error => {
                        console.log(error);
                    })
        }
    }
}
    
    

</script>

<style>
 
</style>
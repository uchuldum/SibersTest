<template>
  <div class="container" >
      <div class="row">
        <div class="col-sm">
            <input type="button" @click = "showProjectAdd" class="btn btn-primary mt-2" value="Добавить проект">
            <modal v-show="isAddProjectVisible" @close="closeProjectAdd">
              <div slot="header">
                Добавление нового проекта
                <button
                  type="button"
                  class="btn-close"
                  @click="closeProjectAdd"
                  aria-label="Close modal"
                  >
                  x
                </button>
              </div>
              <div slot="body">
                <project-add></project-add>
              </div>
              <div slot="footer">
                SibersTest Footer
              </div>
            </modal>
            
            <h1>Проекты</h1>
            <projectList></projectList>
        </div>
        <div class="col-sm">
            <input type="button" @click = "showEmployeeAdd" class="btn btn-primary mt-2" value="Добавить сотрудника">
            <modal v-show="isAddEmployeeVisible" @close="closeEmployeeAdd">
              <div slot="header">
                Добавление нового сотрудника
                <button
                  type="button"
                  class="btn-close"
                  @click="closeEmployeeAdd"
                  aria-label="Close modal"
                  >
                  x
                </button>
              </div>
              <div slot="body">
                <employee-add></employee-add>
              </div>
              <div slot="footer">
                SibersTest Footer
              </div>
            </modal>
            
            <h1>Сотрудники</h1>
        </div>
      </div>
     
    </div>
</template>

<script>

import projectList from "./projectList.vue"
import ProjectAdd from "./ProjectAdd.vue"
import EmployeeAdd from "./employeeAdd.vue"

import modal from './modalAddEmployee.vue';

export default {
  components:{
    projectList,
    ProjectAdd,
    EmployeeAdd,
    modal
  },
  props:{
    project: {
      type: Object,
      default: null
    }
  },
  data () {
    return {
      projectadd : null,
      projectaddbool: true,
      employeeadd : null,
      employeeaddbool: true,
      employees: [],
      projects: [],
      isAddProjectVisible: false,
      isAddEmployeeVisible: false
    }
  },
  methods:{
    addProject(){
      if(this.projectaddbool){
        this.projectaddbool = false;
        this.projectadd = "ProjectAdd";
      }
      else{
        this.projectaddbool = true;
        this.projectadd = null;
      }
      
    },
    addEmployee(){
      if(this.employeeaddbool){
        this.employeeaddbool = false;
        this.employeeadd = "EmployeeAdd";
      }
      else{
        this.employeeaddbool = true;
        this.employeeadd = null;
      }
    },
    showProjectAdd() {
        this.isAddProjectVisible = true;
        console.log("BUTTON")
    },
    closeProjectAdd() {
        this.isAddProjectVisible = false;
    },
    showEmployeeAdd() {
        this.isAddEmployeeVisible = true;
        console.log("BUTTON")
    },
    closeEmployeeAdd() {
        this.isAddEmployeeVisible = false;
    }
  
    
  }
}
</script>

<style scoped>
.btn-close {
  border: none;
  font-size: 20px;
  padding: 20px;
  cursor: pointer;
  font-weight: bold;
  color: #4aae9b;
  background: transparent;
}
</style>

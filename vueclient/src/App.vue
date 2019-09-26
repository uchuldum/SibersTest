<template>
  <div class="container">
      <div class="row">
        <div class="col-sm">
            <h1>Проекты</h1>
        </div>
        <div class="col-sm">
            <h1>Сотрудники</h1>
        </div>
      </div>
      <div class="row">
          <div class="col-sm-4">
                 <project-list></project-list>
          </div>
      </div>
    </div>
</template>

<script>

import projectList from "./projectList.vue"

export default {
  components:{
    projectList
  },
  data () {
    return {
      employees: [],
      projects: []
    }
  }
}
</script>



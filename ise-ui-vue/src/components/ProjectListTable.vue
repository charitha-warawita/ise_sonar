<template lang="">
    <div>
        <table id="tableComponent" class="table table-striped table-hover">
        <thead>
            <tr class="tableHeader">
            <!-- loop through each value of the fields to get the table header -->
            <th v-for="field in fieldTitles" :key='field' @click="sortTable(field)" > 
                <b>{{field}}</b> <i class="bi bi-sort-alpha-down" aria-label='Sort Icon'></i>
            </th>
            </tr>
        </thead>
        <tbody>
            <!-- Loop through the list get the each student data -->
            <tr v-for="item in projects" :key='item'>
            <td v-for="field in fields" :key='field'>{{item[field]}}</td>
            </tr>
        </tbody>
        </table> 
    </div>
</template>
<script>
export default {
    name: 'TableComponent',
    props: {
        projects: {
            type: Array
        },
        fields: {
            type: Array
        },
        fieldTitles: {
            type: Array
        }

    }
    
}
</script>
<style>
    .tableHeader {
        background-color: lightgray;
    }
</style>
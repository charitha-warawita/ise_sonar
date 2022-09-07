<template lang="">
    <div>
        <table id="tableComponent" class="table table-striped table-hover">
        <thead>
            <tr class="tableHeader">
            <!-- loop through each value of the fields to get the table header -->
            <th v-for="field in fieldTitles" :key='field' @click="sortTable(field)" > 
                <b v-if="field==='Fielding Period'">
                    {{field}} (Days)
                </b> 
                <b v-else>
                    {{field}}
                </b> 
                <i class="bi bi-sort-alpha-down" aria-label='Sort Icon'></i>
            </th>
            </tr>
        </thead>
        <tbody>
            <tr v-if="!projects" class="table text-center">
                <td :colspan="fieldTitles.length">
                    No records available!
                </td>
            </tr>
            <!-- Loop through the list get the each student data -->
            <tr v-for="item in projects" :key='item'>
                <td v-for="field in fields" :key='field'>
                    <template v-if="field==='lastUpdate' || field==='startDate'">
                        <template v-if="field==='lastUpdate'">
                            {{formatDate(item[field])}}
                        </template>
                        <template v-else>
                            {{formatDate(item[field],'MM/DD/YYYY')}}
                        </template>
                    </template>
                    
                    <template v-else>
                        {{item[field]}}
                    </template>
                </td>
            </tr>
        </tbody>
        </table> 
    </div>
</template>
<script>
import moment from 'moment'
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
    ,
    methods: {
        formatDate: function(value, format){
          if (value) {
            return moment(String(value)).format(format || 'MM/DD/YYYY hh:mm A');
          }
          else {
            return "";
          }
        }
    }
}
</script>
<!-- <script >

var formatter = {
    date: function (value, format) { 
        if (value) {
            return moment(String(value)).format(format || 'MM/DD/YY');
        }
    },
    time: function (value, format) {
        if (value) {
            return moment(String(value)).format(format || 'h:mm A');
        }
    }
};

Vue.component('format', {
    template: `<span>{{ formatter[fn](value, format) }}</span>`,
    props: ['value', 'fn', 'format'],
    computed: {
        formatter() {
            return formatter;
        }
    }
});

</script> -->

<style>
    .tableHeader {
        background-color: lightgray;
    }
</style>
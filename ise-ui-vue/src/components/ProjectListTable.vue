<script setup>
const props = defineProps({
	projects: {
		required: true,
		type: Array,
	},
	fields: {
		required: true,
		type: Array,
	},
	fieldTitles: {
		required: true,
		type: Array,
	},
	selectable: {
		required: false,
		type: Boolean,
		default: false,
	},
});

const emits = defineEmits(["rowSelected"]);

const RowSelected = (row) => {
	if (!props.selectable) return;

	emits("rowSelected", row);
};
</script>

<template>
	<div>
		<table id="tableComponent" class="table table-striped table-hover">
			<thead>
				<tr class="tableHeader">
					<!-- loop through each value of the fields to get the table header -->
					<th
						v-for="field in props.fieldTitles"
						:key="field"
						@click="sortTable(field)"
					>
						<b v-if="field==='Fielding Period'">
							{{field}} (Days)
						</b> 
						<b v-else>
							{{field}}
						</b> 
						<i
							class="bi bi-sort-alpha-down"
							aria-label="Sort Icon"
						></i>
					</th>
				</tr>
			</thead>
			<tbody>
				<tr v-if="!projects" class="table text-center">
					<td :colspan="fieldTitles.length">
						No records available!
					</td>
				</tr>
				<tr
					v-for="item in props.projects"
					:key="item"
					:class="{ 'table-row-selectable': props.selectable }"
					@click="RowSelected(item)"
				>
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
    },
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
<style>
    .tableHeader {
        background-color: lightgray;
    }
</style>
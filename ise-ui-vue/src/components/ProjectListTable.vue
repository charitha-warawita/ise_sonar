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
						<b>{{ field }}</b>
						<i
							class="bi bi-sort-alpha-down"
							aria-label="Sort Icon"
						></i>
					</th>
				</tr>
			</thead>
			<tbody>
				<tr
					v-for="item in props.projects"
					:key="item"
					:class="{ 'table-row-selectable': props.selectable }"
					@click="RowSelected(item)"
				>
					<td v-for="field in props.fields" :key="field">
						{{ item[field] }}
					</td>
				</tr>
			</tbody>
		</table>
	</div>
</template>

<style scoped>
.tableHeader {
	background-color: lightgray;
}

.table-row-selectable:hover {
	cursor: pointer;
}
</style>

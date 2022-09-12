<script setup>
import { format, parseJSON } from "date-fns";

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
						<b v-if="field === 'Fielding Period'">
							{{ field }} (Days)
						</b>
						<b v-else>
							{{ field }}
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
					<td :colspan="fieldTitles.length">No records available!</td>
				</tr>
				<tr
					v-for="item in props.projects"
					:key="item"
					:class="{ 'table-row-selectable': props.selectable }"
					@click="RowSelected(item)"
				>
					<td v-for="field in fields" :key="field">
						<template
							v-if="
								field === 'lastUpdate' || field === 'startDate'
							"
						>
							<template v-if="field === 'lastUpdate'">
								{{
									format(
										parseJSON(item[field]),
										"yyyy-MM-dd hh:mm aa"
									)
								}}
							</template>
							<template v-else>
								{{
									format(parseJSON(item[field]), "yyyy-MM-dd")
								}}
							</template>
						</template>

						<template v-else>
							{{ item[field] }}
						</template>
					</td>
				</tr>
			</tbody>
		</table>
	</div>
</template>

<style>
.tableHeader {
	background-color: lightgray;
}
.table-hover tbody tr:hover td,
.table-hover tbody tr:hover th {
	background-color: #a4bae3;
	color: white;
	cursor: pointer;
}
</style>

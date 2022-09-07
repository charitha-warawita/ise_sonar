<script setup></script>

<template>
	<vue-awesome-paginate></vue-awesome-paginate>
</template>

<style scoped>
.pagination-container {
	display: flex;
	margin-bottom: 0;
}

.pagination-container ::v-deep(li) {
	margin-right: 5px;
}

::v-deep(.paginate-buttons) {
	height: 40px;
	width: 40px;
	border-radius: 20px;
	cursor: pointer;
	background-color: rgb(242, 242, 242);
	border: 1px solid rgb(217, 217, 217);
	color: black;
}

::v-deep(.paginate-buttons:hover) {
	background-color: #d8d8d8;
}

::v-deep(.active-page) {
	background-color: #0d6efd;
	border: 1px solid #0d6efd;
	color: white;
}

::v-deep(.active-page:hover) {
	background-color: #0d6efd;
}
</style>

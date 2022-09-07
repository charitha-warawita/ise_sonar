<template>
	<div>
		<RouterView />
	</div>
</template>
<script setup>
import { useUserStore } from "@/stores/userStore";
import { onMounted } from "vue";
import { RouterView } from "vue-router";

const userStore = useUserStore();

onMounted(async () => {
	await userStore.created();
	await userStore.fetchGraphDetails();
});
</script>
<style>
@import "@/assets/base.css";

#app {
	max-width: 1280px;
	margin: 0 auto;
	padding: 2rem;
	font-weight: normal;
}

header {
	line-height: 1.5;
	max-height: 100vh;
}

.logo {
	display: block;
	margin: 0 auto 2rem;
}

a {
	/* text-decoration: none; */
	transition: 0.4s;
}

.green {
	text-decoration: none;
	color: hsla(160, 100%, 37%, 1);
	transition: 0.4s;
}

@media (hover: hover) {
	a:hover {
		background-color: hsla(160, 100%, 37%, 0.2);
	}
}

.custom-toggler .navbar-toggler-icon {
	background-image: url("data:image/svg+xml;charset=utf8,%3Csvg viewBox='0 0 32 32' xmlns='http://www.w3.org/2000/svg'%3E%3Cpath stroke='rgba(255,102,203, 0.5)' stroke-width='2' stroke-linecap='round' stroke-miterlimit='10' d='M4 8h24M4 16h24M4 24h24'/%3E%3C/svg%3E");
}

pre {
	white-space: pre-wrap; /* css-3 */
	white-space: -moz-pre-wrap; /* Mozilla, since 1999 */
	white-space: -pre-wrap; /* Opera 4-6 */
	white-space: -o-pre-wrap; /* Opera 7 */
	word-wrap: break-word; /* Internet Explorer 5.5+ */
}
.custom-toggler.navbar-toggler {
	border-color: rgb(255, 102, 203);
}

@media (min-width: 1024px) {
	#app {
		padding: 0 2rem;
	}

	header {
		display: flex;
		place-items: center;
		padding-right: calc(var(--section-gap) / 2);
	}

	header .wrapper {
		display: flex;
		place-items: flex-start;
		flex-wrap: wrap;
	}

	.logo {
		margin: 0 2rem 0 0;
	}

	nav {
		text-align: left;
		margin-left: -1rem;
		font-size: 1rem;

		padding: 1rem 0;
		margin-top: 1rem;
	}
}
</style>

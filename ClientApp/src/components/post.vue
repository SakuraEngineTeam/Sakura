<template>
  <section class="post">
    <div class="post__header">
      <div class="left">
        <span class="post__date">{{ date }}</span>
        <span class="post__id">№{{ post.viewId }}</span>
      </div>
      <div class="right">
        <a class="link" href="#">Reply</a>
      </div>
    </div>

    <div class="post__content">
      <div class="post__message">{{ post.message }}</div>
    </div>
  </section>
</template>

<script lang="ts" src="./post.ts"></script>
<style lang="scss" src="./post.scss"></style>

import { describe, it, expect, beforeEach } from 'vitest'

import { mount, shallowMount } from '@vue/test-utils'
import PageTitle from '../PageTitle.vue'
import HomeView from '@/views/HomeView.vue' 



describe('PageTitle', () => {
    it('PageTitle renders properly-Projects', () => {
    const wrapper = shallowMount(PageTitle, { props: { title: 'Projects' } }) 
    expect(wrapper.text()).toMatch('Projects')
    });
    it('PageTitle renders properly-CreateProjects', () => {
      const wrapper = shallowMount(PageTitle, { props: { title: 'Create Projects' } }) 
      expect(wrapper.text()).toMatch('Create Projects')
    });
    it('PageTitle renders properly-About', () => {
      const wrapper = shallowMount(PageTitle, { props: { title: 'About' } }) 
      expect(wrapper.text()).toMatch('About')
    });   
  })
  


  
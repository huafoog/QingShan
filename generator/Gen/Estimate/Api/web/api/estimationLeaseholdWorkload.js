import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/estimationLeaseholdWorkload/Page',
    method: 'get',
    params
  })
}

export function getById(params) {
  return request({
    url: '/estimationLeaseholdWorkload/getById',
    method: 'get',
    params
  })
}

export function add(data) {
  return request({
    url: '/estimationLeaseholdWorkload/add',
    method: 'post',
    data
  })
}

export function update(data) {
  return request({
    url: '/estimationLeaseholdWorkload/update',
    method: 'post',
    data
  })
}

export function del(data) {
  return request({
    url: '/estimationLeaseholdWorkload/delete',
    method: 'post',
    data
  })
}


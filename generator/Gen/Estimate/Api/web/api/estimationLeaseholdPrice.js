import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/estimationLeaseholdPrice/Page',
    method: 'get',
    params
  })
}

export function getById(params) {
  return request({
    url: '/estimationLeaseholdPrice/getById',
    method: 'get',
    params
  })
}

export function add(data) {
  return request({
    url: '/estimationLeaseholdPrice/add',
    method: 'post',
    data
  })
}

export function update(data) {
  return request({
    url: '/estimationLeaseholdPrice/update',
    method: 'post',
    data
  })
}

export function del(data) {
  return request({
    url: '/estimationLeaseholdPrice/delete',
    method: 'post',
    data
  })
}


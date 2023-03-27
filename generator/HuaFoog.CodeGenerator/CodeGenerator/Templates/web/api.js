import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/${model.Name}/Page',
    method: 'get',
    params
  })
}

export function getById(params) {
  return request({
    url: '/${model.Name}/getById',
    method: 'get',
    params
  })
}

export function add(data) {
  return request({
    url: '/${model.Name}/add',
    method: 'post',
    data
  })
}

export function update(data) {
  return request({
    url: '/${model.Name}/update',
    method: 'post',
    data
  })
}

export function del(data) {
  return request({
    url: '/${model.Name}/delete',
    method: 'post',
    data
  })
}


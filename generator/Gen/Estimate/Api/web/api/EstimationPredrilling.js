import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/estimationPredrilling/Page',
    method: 'get',
    params
  })
}

export function getById(params) {
  return request({
    url: '/estimationPredrilling/getById',
    method: 'get',
    params
  })
}

export function add(data) {
  return request({
    url: '/estimationPredrilling/add',
    method: 'post',
    data
  })
}

export function update(data) {
  return request({
    url: '/estimationPredrilling/update',
    method: 'post',
    data
  })
}

export function del(data) {
  return request({
    url: '/estimationPredrilling/delete',
    method: 'post',
    data
  })
}


﻿<template>
    <div>
        <myDialog :visible.sync="showDialog" :title="initParams.title" width="1000px" top="10vh">
            <el-form ref="form" :model="form" label-width="100px">
                $foreach(item in model.ColumnConfig)
                $if(checkField.IsShowWithId(item.PropName))
                <el-form-item label="${item.Remark}">
                    <el-input v-model="form.${checkField.GetFirstLowerName(item.PropName)}" />
                </el-form-item>
                $end
                $end
            </el-form>
            <div class="flex justify-end margin-top-xs">
                <el-button plain @click="()=>{ this.showDialog = false }">取消</el-button>
                <el-button type="primary" :loading="saveLoading" @click="beforeSave">保存</el-button>
            </div>
        </myDialog>
    </div>
</template>

<script>
import MyDialog from '@/components/system/MyDialog'
import dialogMixin from '@/mixins/dialogMixin'
import { add, update, getById } from '@/api/${model.Name}'
export default {
  components: {
    MyDialog
  },
  mixins: [dialogMixin],
  props: {
    types: {
      type: Array
    }
  },
  data() {
    return {
      options: [
        {
          label: '是',
          value: true
        },
        {
          label: '否',
          value: false
        }
      ],
      saveLoading: false,
      form: {
          $foreach(item in model.ColumnConfig)
          $if(checkField.IsShowWithId(item.PropName))
        ${ checkField.GetFirstLowerName(item.PropName) }: '',
          $end
          $end
      }
    }
  },
  created() {
    this.init()
  },
  methods: {
    init() {
      if (this.initParams.type === 1) {
        getById({ id: this.initParams.id }).then(res => {
          this.form = res.data
        })
      }
    },
    handleReset() {

    },
    beforeSave() {
      this.saveLoading = true
      if (this.initParams.type === 0) {
        add(this.form).then(res => {
          this.saveLoading = false
            if (res.isSuccess) {
                this.showDialog = false
                this.initParams.cb()
                this.${"$"}message.success('操作成功')
            } else {
                this.${"$"}message.error(res.message)
            }
         
        }).catch((err) => {
            this.saveLoading = false
        })
      } else {
        update(this.form).then(res => {
          this.saveLoading = false
        if (res.isSuccess) {
            this.showDialog = false
            this.initParams.cb()
            this.${ "$" }message.success('操作成功')
        } else {
            this.${ "$" }message.error(res.message)
        }
        }).catch((err) => {
            this.saveLoading = false
        })
      }
    }

  }
}
</script>

<style lang="scss" scoped>
</style>


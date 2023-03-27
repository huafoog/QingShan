<template>
    <div>
        <myDialog :visible.sync="showDialog" :title="initParams.title" width="1000px" top="10vh">
            <el-form ref="form" :model="form" label-width="100px">
                $foreach(item in model.ColumnConfig)
                $if(item.PropName == "CreateTime")
                ${ elif(item.PropName == "DeleteTime")}
                ${ elif(item.PropName == "CreatedId")}
                ${ elif(item.PropName == "Id")}
                $else
                <el-form-item label="${item.Remark}">
                    <el-input v-model="form.${item.PropName}" />
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
              $if(item.PropName == "CreateTime")
              ${ elif(item.PropName == "DeleteTime") }
         ${ elif(item.PropName == "CreatedId") }
         ${ elif(item.PropName == "Id") }
      $else
            ${item.PropName}: '',
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
    findTypeList(type) {
      var arr = this.types.filter(item => item.type === type)
      if (arr != null && arr.length > 0) {
        return arr[0].typeList
      }
      return []
    },
    handleReset() {

    },
    beforeSave() {
      this.saveLoading = true
      if (this.initParams.type === 0) {
        add(this.form).then(res => {
          this.saveLoading = false
          this.showDialog = false
          this.initParams.cb()
        })
      } else {
        update(this.form).then(res => {
          this.saveLoading = false
          this.showDialog = false
          this.initParams.cb()
        })
      }
    }

  }
}
</script>

<style lang="scss" scoped>
</style>


# 接口文档

## 用户

### 注册

POST 接收参数：account, nickName, pwd

### 登陆

POST 接收参数：account, pwd

## 文章

### 展示所有文章
GET

### 发布

POST 接收参数：title, content, authorId, guid

### 删除

POST 接收参数：articleId, guid
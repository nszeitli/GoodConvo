
var VueTyperPlugin = window.VueTyper.default

Vue.use(VueTyperPlugin);

new Vue({
    el: '#app',
    data() {
        return {
            info: null,
            loading: true,
            errored: false,
            items: null,
            responsedata: null,
            index: 0
        }
    },
    methods: {
        updateparent: function (item) {
            this.items.push(item);
            this.index++;
        },
        postConversation: function () {
            axios
                .post('/api/Submit',
                    this.items)
                .catch(error => {
                    console.log(error)
                });
            window.location.href = 'Journal/';
        }
    },
    mounted() {
        axios
            .get('/api/Start?coach=Jack')
            .then(response => {
                console.log(response)
                this.items = response.data
                this.index++;
            })
            .catch(error => {
                console.log(error)
                this.errored = true
            })
            .finally(() => this.loading = false)
 

    }
});

Vue.component('gc-item', {
    // camelCase in JavaScript
    props: ['content', 'author', 'compstyle'],
    template: '<div class="gc-item"><div class="gc-author">{{ author }}</div><div class="gc-content">{{ content }}</div></div>'
});

Vue.component('gc-response', {
    // camelCase in JavaScript
    props: ['content', 'author', 'value', 'index'],
    data: function () {
        return {
            input: "...",
        }
    },
    methods: {
        handleSubmit: function () {
            var toSend = this.input;
            var item = { content: "content", author: this.author };

            item.content = toSend;
            this.$emit('eventname', item);
            var a = this;
            axios.get("/api/Response/?value=" + toSend + "&coach=Jack&index=" + 2)
                .then(function (response) {
                    a.$emit('eventname', response.data)
                }).catch(function (error) {
                    // handle error
                    console.log(error);
                })
        }
    },
    template: '<div class="gc-response"><span class="gc-input-label">Type here: </span><input class="gc-entry" type=\'text\' id=\'input1\' v-model="input"></input><button id="submit" v-on:click="handleSubmit">Done</button></div>'
});
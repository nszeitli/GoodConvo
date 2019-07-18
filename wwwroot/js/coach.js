
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
            finished: false,
            longresponse: false,
            numresponse: false,
            index: 0,
        }
    },
    
    methods: {
        updateparent: function (item) {
            if (this.finished) {

            }
            else {
                if (item.type !== undefined) {
                    this.longresponse = false;
                    this.numresponse = false;
                    if (item.type == 'LongText') { this.longresponse = true; }
                    if (item.type == 'Numerical') { this.numresponse = true; }
                }
                this.items.push(item);
                this.index++;
                
                if (item.content.includes("Thats all I have")) {
                    this.finished = true;
                }
            }
            
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
                var lastItem = this.items[this.items.length - 1];
                if (lastItem.type == 'LongText') { this.longresponse = true; }
                if (lastItem.type == 'Numerical') { this.numresponse = true; }
                this.index++;
            })
            .catch(error => {
                console.log(error)
                this.errored = true
            })
            .finally(() => this.loading = false)


    },
    components: {
        'vueSlider': window['vue-slider-component'],
    },
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
            input: "",
        }
    },
    methods: {
        handleSubmit: function () {
            var toSend = this.input;
            var item = { content: "content", author: this.author };

            item.content = toSend;
            this.$emit('eventname', item);
            var a = this;
            axios.get("/api/Response/?value=" + toSend + "&coach=Jack&index=" + this.index)
                .then(function (response) {
 
                    a.$emit('eventname', response.data)
                }).catch(function (error) {
                    // handle error
                    console.log(error);
                })
            this.input = "";
        }
    },
    template: '<div class="gc-response"><span class="gc-input-label"></span><input class="gc-entry" type=\'text\' id=\'input1\' v-model="input"></input><button id="submit" v-on:click="handleSubmit">Done</button></div>'
});

Vue.component('gc-response-long', {
    // camelCase in JavaScript
    props: ['content', 'author', 'value', 'index'],
    data: function () {
        return {
            input: "",
        }
    },
    methods: {
        handleSubmit: function () {
            var toSend = this.input;
            var item = { content: "content", author: this.author };

            item.content = toSend;
            this.$emit('eventname', item);
            var a = this;
            axios.get("/api/Response/?value=" + toSend + "&coach=Jack&index=" + this.index)
                .then(function (response) {

                    a.$emit('eventname', response.data)
                }).catch(function (error) {
                    // handle error
                    console.log(error);
                })
            this.input = "";
        }
    },
    template: '<div class="gc-response"><span class="gc-input-label"></span><textarea rows="4" cols="50" class="gc-entry" type=\'text\' id=\'input1\' v-model="input"></textarea><button id="submit" v-on:click="handleSubmit">Done</button></div>'
});

Vue.component('gc-response-num', {
    // camelCase in JavaScript
    props: ['content', 'author', 'value', 'index'],
    data: function () {
        return {
            input: "",
        }
    },
    methods: {
        handleSubmit: function () {
            var toSend = this.input;
            var item = { content: "content", author: this.author };

            item.content = toSend;
            this.$emit('eventname', item);
            var a = this;
            axios.get("/api/Response/?value=" + toSend + "&coach=Jack&index=" + this.index)
                .then(function (response) {

                    a.$emit('eventname', response.data)
                }).catch(function (error) {
                    // handle error
                    console.log(error);
                })
            this.input = "";
        },
        isNumber: function (evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode !== 46) {
                evt.preventDefault();;
            } else {
                return true;
            }
        }
    },
    template: '<div class="gc-response"><span class="gc-input-label">Num only</span><input class="gc-entry-num" type=\'text\' id=\'input-num\' v-model="input" @keypress="isNumber($event)"></input><button id="submit" v-on:click="handleSubmit">Done</button></div>'
});

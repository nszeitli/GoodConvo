var VueTyperPlugin = window.VueTyper.default

Vue.use(VueTyperPlugin);

new Vue({
    el: '#app',
    data() {
        return {
            selectedcoach: "Jack",
            coachlist: ["Jack", "Jill"],
            info: null,
            loading: true,
            errored: false,
            items: null,
            helditems: null,
            responsedata: null,
            finished: false,
            longresponse: false,
            numresponse: false,
            viewpreviousenabled: false,
            viewprevious: false,
            lastdate: " - ",
            lastconvoitems:null,
            index: 0
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
        changecoach: function (newcoach) {
            this.selectedcoach = newcoach;
            axios
                .get('/api/Start?coach=' + newcoach)
                .then(response => {
                    if (!response.data[0].content.includes("Something got scrambled")) {
                        this.items = response.data
                        var lastItem = this.items[this.items.length - 1];
                        if (lastItem.type == 'LongText') { this.longresponse = true; }
                        if (lastItem.type == 'Numerical') { this.numresponse = true; }
                        this.index = 1;
                    }
                    
                })
                .catch(error => {
                    console.log(error)
                    this.errored = true
                })

            axios
                .get('/Conversations/GetLastConvo?coach=' + this.selectedcoach)
                .then(response => {
                    if (response.data.itemList != null && response.data.itemList.length > 1){
                        this.lastconvoitems = response.data.itemList;
                        this.lastdate = response.data.dateStr;
                        this.viewpreviousenabled = true;
                    }
                    else {
                        this.viewpreviousenabled = false;
                    }

                })
                .catch(error => {
                    console.log(error)
                    this.errored = true
                })
                .finally(() => this.loading = false)

        },
        postConversation: function () {
            axios
                .post('/api/Submit',
                    this.items)
                .catch(error => {
                    console.log(error)
                });
            window.location.href = 'Journal/';
        },
        toggleviewprevious: function () {
            this.viewprevious = !this.viewprevious;
            console.log(this.viewprevious);

            if (this.lastconvoitems != undefined || this.lastconvoitems != null) {
                if (this.viewprevious) {
                    this.helditems = this.items;
                    this.items = this.lastconvoitems;
                }
                else {
                    this.items = this.helditems;
                }
            }
            
        }
    },
    mounted() {
        //Get coachlist
        axios
            .get('/api/Coaches')
            .then(response => {
                if (response.data.length > 1) {
                    this.coachlist = response.data;
                }
                else {
                    this.errored = true;
                }
                
            })
            .catch(error => {
                console.log(error)
                this.errored = true
            })

        axios
            .get('/api/Start?coach=' + this.selectedcoach)
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

        axios
            .get('/Conversations/GetLastConvo?coach=' + this.selectedcoach)
            .then(response => {
                if (response.data.itemList != null || response.data.itemList.length > 1) {
                    this.lastconvoitems = response.data.itemList;
                    this.lastdate = response.data.dateStr;
                    this.viewpreviousenabled = true;
                }
                else {
                    this.viewpreviousenabled = false;
                }

            })
            .catch(error => {
                console.log(error)
                this.errored = true
            })
    },
});

Vue.component('gc-item', {
    // camelCase in JavaScript
    props: ['content', 'author', 'compstyle'],
    template: '<div class="gc-item"><div class="gc-author">{{ author }}</div><div class="gc-content">{{ content }}</div></div>'
});

Vue.component('gc-response', {
    // camelCase in JavaScript
    props: ['content', 'author', 'value', 'index', 'coach'],
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
            axios.get("/api/Response/?value=" + toSend + "&coach=" + this.coach + "&index=" + this.index)
                .then(function (response) {
 
                    a.$emit('eventname', response.data)
                }).catch(function (error) {
                    // handle error
                    console.log(error);
                })
            this.input = "";
        }
    },
    template: '<div class="gc-response"><input class="gc-entry" type=\'text\' id=\'input1\' v-model="input"></input><button id="submit" v-on:click="handleSubmit"> <i class="fa fa-pencil"></i></button></div>'
});

Vue.component('gc-response-long', {
    props: ['content', 'author', 'value', 'index', 'coach'],
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
            axios.get("/api/Response/?value=" + toSend + "&coach=" + this.coach + "&index=" + this.index)
                .then(function (response) {

                    a.$emit('eventname', response.data)
                }).catch(function (error) {
                    // handle error
                    console.log(error);
                })
            this.input = "";
        }
    },
    template: '<div class="gc-response"><span class="gc-input-label"></span><textarea rows="4" cols="50" class="gc-entry" type=\'text\' id=\'input1\' v-model="input"></textarea><button id="submit" v-on:click="handleSubmit"><i class="fa fa-pencil"></i></button></div>'
});

Vue.component('gc-response-num', {
    // camelCase in JavaScript
    props: ['content', 'author', 'value', 'index', 'coach'],
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
            axios.get("/api/Response/?value=" + toSend + "&coach=" + this.coach + "&index=" + this.index)
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
    template: '<div class="gc-response"><span class="gc-input-label">Num only</span><input class="gc-entry-num" type=\'text\' id=\'input-num\' v-model="input" @keypress="isNumber($event)"></input><button id="submit" v-on:click="handleSubmit"><i class="fa fa-pencil" ></i></button></div>'
});


Vue.component("dropdown", {
    props: ['selectedcoach', 'coachlist'],
    template: "#dropdown",
    data() {
        return {
            showDropDown: false,
        };
    },
    methods: {
        changecoach: function (coachname) {
            this.$emit('changecoach', coachname);
            this.showDropDown = false;
        }
    }
});

Vue.component("dropdown-item", {
    props: ['coachname'],
    template: "<a href=\"#\" v-on:click=\"changecoach\" class=\"dropdown-item\">{{ this.coachname }}</a>",
    data() {
        return {
            showDropDown: false,
        };
    },
    methods: {
        changecoach: function () {
            this.$emit('changecoach', this.coachname);
        }
    }
});

Vue.component("view-previous", {
    props: ['date', 'viewprevious'],
    template: "#previous",
    data() {
        return {
            leftcaret: this.viewprevious
        };
    },
    methods: {
        toggleviewprevious: function () {
            this.$emit('toggleviewprevious');
        }
    }
});
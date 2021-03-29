/*global
    piranha
*/

piranha.clubpermissions = {
    loaded: false,
    clubPages: {
        add: false,
        delete: false,
        edit: false,
        preview: false,
        publish: false,
        save: false
    },

    load: function (cb) {
        var self = this;

        if (!this.loaded) {
            fetch(piranha.baseUrl + "manager/api/clubs/permissions")
                .then(function (response) { return response.json(); })
                .then(function (result) {
                    self.clubPages = result.clubPages;
                    self.loaded = true;

                    if (cb)
                        cb();
                })
                .catch(function (error) {
                    console.log("error:", error);

                    if (cb)
                        cb();
                });
        } else {
            if (cb)
                cb();
        }
    }
};
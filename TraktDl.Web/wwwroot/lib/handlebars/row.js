(function() {
  var template = Handlebars.template, templates = Handlebars.templates = Handlebars.templates || {};
templates['row'] = template({"1":function(container,depth0,helpers,partials,data,blockParams,depths) {
    var stack1, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return ((stack1 = lookupProperty(helpers,"if").call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? lookupProperty(depth0,"haveMissingEpisodes") : depth0),{"name":"if","hash":{},"fn":container.program(2, data, 0, blockParams, depths),"inverse":container.noop,"data":data,"loc":{"start":{"line":26,"column":20},"end":{"line":62,"column":27}}})) != null ? stack1 : "");
},"2":function(container,depth0,helpers,partials,data,blockParams,depths) {
    var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, alias3="function", alias4=container.escapeExpression, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "                    <div class=\"grid-item\" data-season-id=\""
    + alias4(((helper = (helper = lookupProperty(helpers,"id") || (depth0 != null ? lookupProperty(depth0,"id") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data,"loc":{"start":{"line":27,"column":59},"end":{"line":27,"column":67}}}) : helper)))
    + "\">\r\n                        <div class=\"info\">\r\n                            <div class=\"row\">\r\n                                <div class=\"col-xs-11 col-offset-xs-1\">\r\n                                    <span class=\"season-toggle\" data-show-id=\""
    + alias4(container.lambda((depths[1] != null ? lookupProperty(depths[1],"id") : depths[1]), depth0))
    + "\" data-target=\"#season-"
    + alias4(((helper = (helper = lookupProperty(helpers,"id") || (depth0 != null ? lookupProperty(depth0,"id") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data,"loc":{"start":{"line":31,"column":112},"end":{"line":31,"column":120}}}) : helper)))
    + "\" data-toggle=\"collapse\">\r\n                                        <span class=\"\">Season "
    + alias4(((helper = (helper = lookupProperty(helpers,"seasonNumber") || (depth0 != null ? lookupProperty(depth0,"seasonNumber") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"seasonNumber","hash":{},"data":data,"loc":{"start":{"line":32,"column":62},"end":{"line":32,"column":80}}}) : helper)))
    + "</span>\r\n                                        <span class=\"episode-count\">"
    + alias4(((helper = (helper = lookupProperty(helpers,"collectedEpisodes") || (depth0 != null ? lookupProperty(depth0,"collectedEpisodes") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"collectedEpisodes","hash":{},"data":data,"loc":{"start":{"line":33,"column":68},"end":{"line":33,"column":91}}}) : helper)))
    + "/"
    + alias4(((helper = (helper = lookupProperty(helpers,"totalEpisodes") || (depth0 != null ? lookupProperty(depth0,"totalEpisodes") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"totalEpisodes","hash":{},"data":data,"loc":{"start":{"line":33,"column":92},"end":{"line":33,"column":111}}}) : helper)))
    + " episodes</span>\r\n                                    </span>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"row\">\r\n                            <div class=\"col-xs-11\">\r\n                                <div class=\"progress\">\r\n                                    <div aria-valuemax=\"100\" aria-valuemin=\"0\" aria-valuenow=\""
    + alias4(((helper = (helper = lookupProperty(helpers,"percent") || (depth0 != null ? lookupProperty(depth0,"percent") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data,"loc":{"start":{"line":41,"column":94},"end":{"line":41,"column":107}}}) : helper)))
    + "\" class=\"progress-bar progress-bar-striped progress-bar-"
    + alias4((lookupProperty(helpers,"progress-status")||(depth0 && lookupProperty(depth0,"progress-status"))||alias2).call(alias1,(depth0 != null ? lookupProperty(depth0,"percent") : depth0),{"name":"progress-status","hash":{},"data":data,"loc":{"start":{"line":41,"column":163},"end":{"line":41,"column":190}}}))
    + "\" role=\"progressbar\" style=\"width: "
    + alias4(((helper = (helper = lookupProperty(helpers,"percent") || (depth0 != null ? lookupProperty(depth0,"percent") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data,"loc":{"start":{"line":41,"column":225},"end":{"line":41,"column":238}}}) : helper)))
    + "%;\">\r\n                                        <div class=\"sr-only\">"
    + alias4(((helper = (helper = lookupProperty(helpers,"percent") || (depth0 != null ? lookupProperty(depth0,"percent") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data,"loc":{"start":{"line":42,"column":61},"end":{"line":42,"column":74}}}) : helper)))
    + "%</div>\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"col-xs-1 percentage\">"
    + alias4(((helper = (helper = lookupProperty(helpers,"percent") || (depth0 != null ? lookupProperty(depth0,"percent") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data,"loc":{"start":{"line":46,"column":61},"end":{"line":46,"column":74}}}) : helper)))
    + "%</div>\r\n                        </div>\r\n                        <div class=\"row collapse\" id=\"season-"
    + alias4(((helper = (helper = lookupProperty(helpers,"id") || (depth0 != null ? lookupProperty(depth0,"id") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data,"loc":{"start":{"line":48,"column":61},"end":{"line":48,"column":69}}}) : helper)))
    + "\">\r\n                            <div class=\"col-sm-11 col-xs-12\">\r\n                                <div class=\"episodes\">\r\n"
    + ((stack1 = lookupProperty(helpers,"each").call(alias1,(depth0 != null ? lookupProperty(depth0,"episodes") : depth0),{"name":"each","hash":{},"fn":container.program(3, data, 0, blockParams, depths),"inverse":container.noop,"data":data,"loc":{"start":{"line":51,"column":36},"end":{"line":57,"column":45}}})) != null ? stack1 : "")
    + "                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n";
},"3":function(container,depth0,helpers,partials,data,blockParams,depths) {
    var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, alias3=container.escapeExpression, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "                                    <div class=\"episode "
    + alias3((lookupProperty(helpers,"episode-status")||(depth0 && lookupProperty(depth0,"episode-status"))||alias2).call(alias1,(depth0 != null ? lookupProperty(depth0,"status") : depth0),{"name":"episode-status","hash":{},"data":data,"loc":{"start":{"line":52,"column":56},"end":{"line":52,"column":81}}}))
    + "\" data-episode-id=\""
    + alias3(((helper = (helper = lookupProperty(helpers,"id") || (depth0 != null ? lookupProperty(depth0,"id") : depth0)) != null ? helper : alias2),(typeof helper === "function" ? helper.call(alias1,{"name":"id","hash":{},"data":data,"loc":{"start":{"line":52,"column":100},"end":{"line":52,"column":108}}}) : helper)))
    + "\">\r\n                                        <a href=\"#\">\r\n                                            "
    + alias3(container.lambda((depths[1] != null ? lookupProperty(depths[1],"seasonNumber") : depths[1]), depth0))
    + "x"
    + alias3((lookupProperty(helpers,"formatNumber")||(depth0 && lookupProperty(depth0,"formatNumber"))||alias2).call(alias1,(depth0 != null ? lookupProperty(depth0,"episodeNumber") : depth0),{"name":"formatNumber","hash":{"minimumIntegerDigits":2,"useGrouping":false},"data":data,"loc":{"start":{"line":54,"column":66},"end":{"line":54,"column":138}}}))
    + "\r\n                                        </a>\r\n                                    </div>\r\n";
},"5":function(container,depth0,helpers,partials,data) {
    var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, alias3="function", alias4=container.escapeExpression, alias5=container.lambda, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "        <div id=\"show-wait-fanart-"
    + alias4(((helper = (helper = lookupProperty(helpers,"id") || (depth0 != null ? lookupProperty(depth0,"id") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data,"loc":{"start":{"line":68,"column":34},"end":{"line":68,"column":42}}}) : helper)))
    + "\" class=\"grid-item col-md-6 col-sm-4 no-border\">\r\n            <div class=\"fanart\">\r\n                <img class=\"base\" src=\"/images/episodes.png\" alt=\"Fanart\">\r\n                <img id=\"show-collect-poster-"
    + alias4(((helper = (helper = lookupProperty(helpers,"id") || (depth0 != null ? lookupProperty(depth0,"id") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data,"loc":{"start":{"line":71,"column":45},"end":{"line":71,"column":53}}}) : helper)))
    + "\" class=\"real\" src=\""
    + alias4(alias5(((stack1 = (depth0 != null ? lookupProperty(depth0,"nextEpisodeToCollect") : depth0)) != null ? lookupProperty(stack1,"posterUrl") : stack1), depth0))
    + "\" alt=\"Fanart\" style=\"display: block;\">\r\n                <div class=\"loading\">\r\n                    <div class=\"icon\">\r\n                        <div class=\"fa fa-refresh fa-spin\"></div>\r\n                    </div>\r\n                </div>\r\n                <div class=\"shadow-base\"></div>\r\n                <div class=\"titles\">\r\n                    <h4><span class=\"convert-date\" id=\"show-collect-date-"
    + alias4(((helper = (helper = lookupProperty(helpers,"id") || (depth0 != null ? lookupProperty(depth0,"id") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data,"loc":{"start":{"line":79,"column":73},"end":{"line":79,"column":81}}}) : helper)))
    + "\">"
    + alias4(alias5(((stack1 = (depth0 != null ? lookupProperty(depth0,"nextEpisodeToCollect") : depth0)) != null ? lookupProperty(stack1,"airDate") : stack1), depth0))
    + "</span></h4>\r\n                    <h3>\r\n                        <span class=\"main-title-sxe\">\r\n                            "
    + alias4(alias5(((stack1 = (depth0 != null ? lookupProperty(depth0,"nextEpisodeToCollect") : depth0)) != null ? lookupProperty(stack1,"seasonNumber") : stack1), depth0))
    + "x"
    + alias4((lookupProperty(helpers,"formatNumber")||(depth0 && lookupProperty(depth0,"formatNumber"))||alias2).call(alias1,((stack1 = (depth0 != null ? lookupProperty(depth0,"nextEpisodeToCollect") : depth0)) != null ? lookupProperty(stack1,"episodeNumber") : stack1),{"name":"formatNumber","hash":{"minimumIntegerDigits":2,"useGrouping":false},"data":data,"loc":{"start":{"line":82,"column":66},"end":{"line":82,"column":159}}}))
    + "\r\n                        </span> <span class=\"main-title\" id=\"show-collect-name-"
    + alias4(((helper = (helper = lookupProperty(helpers,"id") || (depth0 != null ? lookupProperty(depth0,"id") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data,"loc":{"start":{"line":83,"column":79},"end":{"line":83,"column":87}}}) : helper)))
    + "\">"
    + alias4((lookupProperty(helpers,"truncate")||(depth0 && lookupProperty(depth0,"truncate"))||alias2).call(alias1,((stack1 = (depth0 != null ? lookupProperty(depth0,"nextEpisodeToCollect") : depth0)) != null ? lookupProperty(stack1,"name") : stack1),{"name":"truncate","hash":{},"data":data,"loc":{"start":{"line":83,"column":89},"end":{"line":83,"column":127}}}))
    + "</span>\r\n                    </h3>\r\n                </div>\r\n            </div>\r\n        </div>\r\n";
},"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data,blockParams,depths) {
    var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, alias3="function", alias4=container.escapeExpression, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return " <div class=\"row posters fanarts twenty-four-cols grid-item no-overlays\">\r\n        <div id=\"show-wait-poster-"
    + alias4(((helper = (helper = lookupProperty(helpers,"id") || (depth0 != null ? lookupProperty(depth0,"id") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data,"loc":{"start":{"line":2,"column":34},"end":{"line":2,"column":42}}}) : helper)))
    + "\" class=\"col-md-3 hidden-xs hidden-sm\">\r\n            <div class=\"poster\">\r\n                <img class=\"base\" src=\"/images/posters.png\" alt=\"Fanart\">\r\n                <img class=\"real\" id=\"show-poster-"
    + alias4(((helper = (helper = lookupProperty(helpers,"id") || (depth0 != null ? lookupProperty(depth0,"id") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data,"loc":{"start":{"line":5,"column":50},"end":{"line":5,"column":58}}}) : helper)))
    + "\" src=\""
    + alias4(((helper = (helper = lookupProperty(helpers,"posterUrl") || (depth0 != null ? lookupProperty(depth0,"posterUrl") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"posterUrl","hash":{},"data":data,"loc":{"start":{"line":5,"column":65},"end":{"line":5,"column":80}}}) : helper)))
    + "\" alt=\"Poster\" style=\"display: block;\">\r\n            </div>\r\n        </div>\r\n        <div class=\"col-md-15 col-sm-8 main-info\">\r\n            <h3 class=\"show-title\">\r\n                "
    + alias4(((helper = (helper = lookupProperty(helpers,"serieName") || (depth0 != null ? lookupProperty(depth0,"serieName") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"serieName","hash":{},"data":data,"loc":{"start":{"line":10,"column":16},"end":{"line":10,"column":31}}}) : helper)))
    + "\r\n            </h3>\r\n            <div class=\"row\">\r\n                <div class=\"col-xs-11\">\r\n                    <div class=\"progress\">\r\n                        <div aria-valuemax=\"100\" aria-valuemin=\"0\" aria-valuenow=\""
    + alias4(((helper = (helper = lookupProperty(helpers,"percent") || (depth0 != null ? lookupProperty(depth0,"percent") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data,"loc":{"start":{"line":15,"column":82},"end":{"line":15,"column":95}}}) : helper)))
    + "\" class=\"progress-bar progress-bar-striped progress-bar-"
    + alias4((lookupProperty(helpers,"progress-status")||(depth0 && lookupProperty(depth0,"progress-status"))||alias2).call(alias1,(depth0 != null ? lookupProperty(depth0,"percent") : depth0),{"name":"progress-status","hash":{},"data":data,"loc":{"start":{"line":15,"column":151},"end":{"line":15,"column":178}}}))
    + "\" role=\"progressbar\" style=\"width: "
    + alias4(((helper = (helper = lookupProperty(helpers,"percent") || (depth0 != null ? lookupProperty(depth0,"percent") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data,"loc":{"start":{"line":15,"column":213},"end":{"line":15,"column":226}}}) : helper)))
    + "%\">\r\n                            <div class=\"sr-only\">"
    + alias4(((helper = (helper = lookupProperty(helpers,"percent") || (depth0 != null ? lookupProperty(depth0,"percent") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data,"loc":{"start":{"line":16,"column":49},"end":{"line":16,"column":62}}}) : helper)))
    + "%</div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n                <div class=\"col-xs-1 percentage\">"
    + alias4(((helper = (helper = lookupProperty(helpers,"percent") || (depth0 != null ? lookupProperty(depth0,"percent") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data,"loc":{"start":{"line":20,"column":49},"end":{"line":20,"column":62}}}) : helper)))
    + "%</div>\r\n            </div>\r\n            <p>Collected <strong>"
    + alias4(((helper = (helper = lookupProperty(helpers,"collectedEpisodes") || (depth0 != null ? lookupProperty(depth0,"collectedEpisodes") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"collectedEpisodes","hash":{},"data":data,"loc":{"start":{"line":22,"column":33},"end":{"line":22,"column":56}}}) : helper)))
    + "</strong> of <strong>"
    + alias4(((helper = (helper = lookupProperty(helpers,"totalEpisodes") || (depth0 != null ? lookupProperty(depth0,"totalEpisodes") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"totalEpisodes","hash":{},"data":data,"loc":{"start":{"line":22,"column":77},"end":{"line":22,"column":96}}}) : helper)))
    + "</strong> episodes, which leaves <strong>"
    + alias4(((helper = (helper = lookupProperty(helpers,"missingEpisodes") || (depth0 != null ? lookupProperty(depth0,"missingEpisodes") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"missingEpisodes","hash":{},"data":data,"loc":{"start":{"line":22,"column":137},"end":{"line":22,"column":158}}}) : helper)))
    + "</strong> episodes left to collect.</p>\r\n            <div class=\"seasons\">\r\n                <div class=\"collapse in\" id=\"show-"
    + alias4(((helper = (helper = lookupProperty(helpers,"id") || (depth0 != null ? lookupProperty(depth0,"id") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data,"loc":{"start":{"line":24,"column":50},"end":{"line":24,"column":58}}}) : helper)))
    + "\" style=\"\">\r\n"
    + ((stack1 = lookupProperty(helpers,"each").call(alias1,(depth0 != null ? lookupProperty(depth0,"seasons") : depth0),{"name":"each","hash":{},"fn":container.program(1, data, 0, blockParams, depths),"inverse":container.noop,"data":data,"loc":{"start":{"line":25,"column":20},"end":{"line":63,"column":29}}})) != null ? stack1 : "")
    + "                </div>\r\n            </div>\r\n        </div>\r\n"
    + ((stack1 = lookupProperty(helpers,"if").call(alias1,(depth0 != null ? lookupProperty(depth0,"nextEpisodeToCollect") : depth0),{"name":"if","hash":{},"fn":container.program(5, data, 0, blockParams, depths),"inverse":container.noop,"data":data,"loc":{"start":{"line":67,"column":8},"end":{"line":88,"column":15}}})) != null ? stack1 : "")
    + "    </div>";
},"useData":true,"useDepths":true});
})();
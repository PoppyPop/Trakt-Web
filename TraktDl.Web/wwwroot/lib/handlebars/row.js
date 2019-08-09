(function() {
  var template = Handlebars.template, templates = Handlebars.templates = Handlebars.templates || {};
templates['row'] = template({"1":function(container,depth0,helpers,partials,data,blockParams,depths) {
    var stack1;

  return ((stack1 = helpers["if"].call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? depth0.haveMissingEpisodes : depth0),{"name":"if","hash":{},"fn":container.program(2, data, 0, blockParams, depths),"inverse":container.noop,"data":data})) != null ? stack1 : "");
},"2":function(container,depth0,helpers,partials,data,blockParams,depths) {
    var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=helpers.helperMissing, alias3="function", alias4=container.escapeExpression;

  return "                    <div class=\"grid-item\" data-season-id=\""
    + alias4(((helper = (helper = helpers.id || (depth0 != null ? depth0.id : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data}) : helper)))
    + "\">\r\n                        <div class=\"info\">\r\n                            <div class=\"row\">\r\n                                <div class=\"col-xs-11 col-offset-xs-1\">\r\n                                    <span class=\"season-toggle\" data-show-id=\""
    + alias4(container.lambda((depths[1] != null ? depths[1].id : depths[1]), depth0))
    + "\" data-target=\"#season-"
    + alias4(((helper = (helper = helpers.id || (depth0 != null ? depth0.id : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data}) : helper)))
    + "\" data-toggle=\"collapse\">\r\n                                        <span class=\"\">Season "
    + alias4(((helper = (helper = helpers.seasonNumber || (depth0 != null ? depth0.seasonNumber : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"seasonNumber","hash":{},"data":data}) : helper)))
    + "</span>\r\n                                        <span class=\"episode-count\">"
    + alias4(((helper = (helper = helpers.collectedEpisodes || (depth0 != null ? depth0.collectedEpisodes : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"collectedEpisodes","hash":{},"data":data}) : helper)))
    + "/"
    + alias4(((helper = (helper = helpers.totalEpisodes || (depth0 != null ? depth0.totalEpisodes : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"totalEpisodes","hash":{},"data":data}) : helper)))
    + " episodes</span>\r\n                                    </span>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"row\">\r\n                            <div class=\"col-xs-11\">\r\n                                <div class=\"progress\">\r\n                                    <div aria-valuemax=\"100\" aria-valuemin=\"0\" aria-valuenow=\""
    + alias4(((helper = (helper = helpers.percent || (depth0 != null ? depth0.percent : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data}) : helper)))
    + "\" class=\"progress-bar progress-bar-striped progress-bar-"
    + alias4((helpers["progress-status"] || (depth0 && depth0["progress-status"]) || alias2).call(alias1,(depth0 != null ? depth0.percent : depth0),{"name":"progress-status","hash":{},"data":data}))
    + "\" role=\"progressbar\" style=\"width: "
    + alias4(((helper = (helper = helpers.percent || (depth0 != null ? depth0.percent : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data}) : helper)))
    + "%;\">\r\n                                        <div class=\"sr-only\">"
    + alias4(((helper = (helper = helpers.percent || (depth0 != null ? depth0.percent : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data}) : helper)))
    + "%</div>\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"col-xs-1 percentage\">"
    + alias4(((helper = (helper = helpers.percent || (depth0 != null ? depth0.percent : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data}) : helper)))
    + "%</div>\r\n                        </div>\r\n                        <div class=\"row collapse\" id=\"season-"
    + alias4(((helper = (helper = helpers.id || (depth0 != null ? depth0.id : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data}) : helper)))
    + "\">\r\n                            <div class=\"col-sm-11 col-xs-12\">\r\n                                <div class=\"episodes\">\r\n"
    + ((stack1 = helpers.each.call(alias1,(depth0 != null ? depth0.episodes : depth0),{"name":"each","hash":{},"fn":container.program(3, data, 0, blockParams, depths),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n";
},"3":function(container,depth0,helpers,partials,data,blockParams,depths) {
    var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=helpers.helperMissing, alias3=container.escapeExpression;

  return "                                    <div class=\"episode "
    + alias3((helpers["episode-status"] || (depth0 && depth0["episode-status"]) || alias2).call(alias1,(depth0 != null ? depth0.status : depth0),{"name":"episode-status","hash":{},"data":data}))
    + "\" data-episode-id=\""
    + alias3(((helper = (helper = helpers.id || (depth0 != null ? depth0.id : depth0)) != null ? helper : alias2),(typeof helper === "function" ? helper.call(alias1,{"name":"id","hash":{},"data":data}) : helper)))
    + "\">\r\n                                        <a href=\"#\">\r\n                                            "
    + alias3(container.lambda((depths[1] != null ? depths[1].seasonNumber : depths[1]), depth0))
    + "x"
    + alias3((helpers.formatNumber || (depth0 && depth0.formatNumber) || alias2).call(alias1,(depth0 != null ? depth0.episodeNumber : depth0),{"name":"formatNumber","hash":{"minimumIntegerDigits":2,"useGrouping":false},"data":data}))
    + "\r\n                                        </a>\r\n                                    </div>\r\n";
},"5":function(container,depth0,helpers,partials,data) {
    var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=helpers.helperMissing, alias3="function", alias4=container.escapeExpression, alias5=container.lambda;

  return "        <div id=\"show-wait-fanart-"
    + alias4(((helper = (helper = helpers.id || (depth0 != null ? depth0.id : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data}) : helper)))
    + "\" class=\"grid-item col-md-6 col-sm-4 no-border\">\r\n            <div class=\"fanart\">\r\n                <img class=\"base\" src=\"/images/episodes.png\" alt=\"Fanart\">\r\n                <img id=\"show-collect-poster-"
    + alias4(((helper = (helper = helpers.id || (depth0 != null ? depth0.id : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data}) : helper)))
    + "\" class=\"real\" src=\""
    + alias4(alias5(((stack1 = (depth0 != null ? depth0.nextEpisodeToCollect : depth0)) != null ? stack1.posterUrl : stack1), depth0))
    + "\" alt=\"Fanart\" style=\"display: block;\">\r\n                <div class=\"loading\">\r\n                    <div class=\"icon\">\r\n                        <div class=\"fa fa-refresh fa-spin\"></div>\r\n                    </div>\r\n                </div>\r\n                <div class=\"shadow-base\"></div>\r\n                <div class=\"titles\">\r\n                    <h4><span class=\"convert-date\" id=\"show-collect-date-"
    + alias4(((helper = (helper = helpers.id || (depth0 != null ? depth0.id : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data}) : helper)))
    + "\">"
    + alias4(alias5(((stack1 = (depth0 != null ? depth0.nextEpisodeToCollect : depth0)) != null ? stack1.airDate : stack1), depth0))
    + "</span></h4>\r\n                    <h3>\r\n                        <span class=\"main-title-sxe\">\r\n                            "
    + alias4(alias5(((stack1 = (depth0 != null ? depth0.nextEpisodeToCollect : depth0)) != null ? stack1.seasonNumber : stack1), depth0))
    + "x"
    + alias4((helpers.formatNumber || (depth0 && depth0.formatNumber) || alias2).call(alias1,((stack1 = (depth0 != null ? depth0.nextEpisodeToCollect : depth0)) != null ? stack1.episodeNumber : stack1),{"name":"formatNumber","hash":{"minimumIntegerDigits":2,"useGrouping":false},"data":data}))
    + "\r\n                        </span> <span class=\"main-title\" id=\"show-collect-name-"
    + alias4(((helper = (helper = helpers.id || (depth0 != null ? depth0.id : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data}) : helper)))
    + "\">"
    + alias4((helpers.truncate || (depth0 && depth0.truncate) || alias2).call(alias1,((stack1 = (depth0 != null ? depth0.nextEpisodeToCollect : depth0)) != null ? stack1.name : stack1),{"name":"truncate","hash":{},"data":data}))
    + "</span>\r\n                    </h3>\r\n                </div>\r\n            </div>\r\n        </div>\r\n";
},"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data,blockParams,depths) {
    var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=helpers.helperMissing, alias3="function", alias4=container.escapeExpression;

  return " <div class=\"row posters fanarts twenty-four-cols grid-item no-overlays\">\r\n        <div id=\"show-wait-poster-"
    + alias4(((helper = (helper = helpers.id || (depth0 != null ? depth0.id : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data}) : helper)))
    + "\" class=\"col-md-3 hidden-xs hidden-sm\">\r\n            <div class=\"poster\">\r\n                <img class=\"base\" src=\"/images/posters.png\" alt=\"Fanart\">\r\n                <img class=\"real\" id=\"show-poster-"
    + alias4(((helper = (helper = helpers.id || (depth0 != null ? depth0.id : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data}) : helper)))
    + "\" src=\""
    + alias4(((helper = (helper = helpers.posterUrl || (depth0 != null ? depth0.posterUrl : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"posterUrl","hash":{},"data":data}) : helper)))
    + "\" alt=\"Poster\" style=\"display: block;\">\r\n            </div>\r\n        </div>\r\n        <div class=\"col-md-15 col-sm-8 main-info\">\r\n            <h3 class=\"show-title\">\r\n                "
    + alias4(((helper = (helper = helpers.serieName || (depth0 != null ? depth0.serieName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"serieName","hash":{},"data":data}) : helper)))
    + "\r\n            </h3>\r\n            <div class=\"row\">\r\n                <div class=\"col-xs-11\">\r\n                    <div class=\"progress\">\r\n                        <div aria-valuemax=\"100\" aria-valuemin=\"0\" aria-valuenow=\""
    + alias4(((helper = (helper = helpers.percent || (depth0 != null ? depth0.percent : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data}) : helper)))
    + "\" class=\"progress-bar progress-bar-striped progress-bar-"
    + alias4((helpers["progress-status"] || (depth0 && depth0["progress-status"]) || alias2).call(alias1,(depth0 != null ? depth0.percent : depth0),{"name":"progress-status","hash":{},"data":data}))
    + "\" role=\"progressbar\" style=\"width: "
    + alias4(((helper = (helper = helpers.percent || (depth0 != null ? depth0.percent : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data}) : helper)))
    + "%\">\r\n                            <div class=\"sr-only\">"
    + alias4(((helper = (helper = helpers.percent || (depth0 != null ? depth0.percent : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data}) : helper)))
    + "%</div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n                <div class=\"col-xs-1 percentage\">"
    + alias4(((helper = (helper = helpers.percent || (depth0 != null ? depth0.percent : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percent","hash":{},"data":data}) : helper)))
    + "%</div>\r\n            </div>\r\n            <p>Collected <strong>"
    + alias4(((helper = (helper = helpers.collectedEpisodes || (depth0 != null ? depth0.collectedEpisodes : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"collectedEpisodes","hash":{},"data":data}) : helper)))
    + "</strong> of <strong>"
    + alias4(((helper = (helper = helpers.totalEpisodes || (depth0 != null ? depth0.totalEpisodes : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"totalEpisodes","hash":{},"data":data}) : helper)))
    + "</strong> episodes, which leaves <strong>"
    + alias4(((helper = (helper = helpers.missingEpisodes || (depth0 != null ? depth0.missingEpisodes : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"missingEpisodes","hash":{},"data":data}) : helper)))
    + "</strong> episodes left to collect.</p>\r\n            <div class=\"seasons\">\r\n                <div class=\"collapse in\" id=\"show-"
    + alias4(((helper = (helper = helpers.id || (depth0 != null ? depth0.id : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data}) : helper)))
    + "\" style=\"\">\r\n"
    + ((stack1 = helpers.each.call(alias1,(depth0 != null ? depth0.seasons : depth0),{"name":"each","hash":{},"fn":container.program(1, data, 0, blockParams, depths),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "                </div>\r\n            </div>\r\n        </div>\r\n"
    + ((stack1 = helpers["if"].call(alias1,(depth0 != null ? depth0.nextEpisodeToCollect : depth0),{"name":"if","hash":{},"fn":container.program(5, data, 0, blockParams, depths),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "    </div>";
},"useData":true,"useDepths":true});
})();
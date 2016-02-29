angular.module("umbraco.resources")
	.factory("approvalResource", function ($http) {

	    return {
	        getNodeById: function (id, userLocale) {
	            return $http.get("backoffice/ApproveIt/ApprovalApi/GetNodeById?id=" + id + "&userLocale=" + userLocale);
	        },
	        getById: function (id, userLocale) {
	            return $http.get("backoffice/ApproveIt/ApprovalApi/GetById?id=" + id + "&userLocale=" + userLocale);
	        },
	        publish: function (id) {
	            return $http.post("backoffice/ApproveIt/ApprovalApi/PostPublish?id=" + id);
	        }
	    };
	});
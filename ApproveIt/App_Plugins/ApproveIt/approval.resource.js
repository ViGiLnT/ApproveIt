angular.module("umbraco.resources")
	.factory("approvalResource", function ($http) {

	    return {
	        getNodeById: function (id, userLocale) {
	            return $http.get("backoffice/ApproveIt/ApprovalApi/GetNodeById?id=" + id + "&userLocale=" + userLocale);
	        },
	        getPropertyById: function (id, property, userLocale) {
	            return $http.get("backoffice/ApproveIt/ApprovalApi/GetPropertyById?id=" + id + "&property=" + property + "&userLocale=" + userLocale);
	        },
	        publish: function (id) {
	            return $http.post("backoffice/ApproveIt/ApprovalApi/PostPublish?id=" + id);
	        }
	    };
	});
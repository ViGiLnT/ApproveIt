angular.module("umbraco.resources")
	.factory("approvalResource", function ($http) {
	    return {
	        getById: function (id) {
	            return $http.get("backoffice/ApproveIt/ApprovalApi/GetById?id=" + id);
	        },
	        publish: function (id) {
	            return $http.post("backoffice/ApproveIt/ApprovalApi/PostPublish?id=" + id);
	        }
	    };
	});
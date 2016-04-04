angular.module("umbraco").controller("Approval.ApprovalHistoryController",
	function ($scope, $routeParams, approvalResource, notificationsService, navigationService, userService, entityResource) {

	    $scope.loaded = false;
	    if ($scope.$parent.menuNode !== null) {
	        var parentId = $scope.$parent.menuNode.parentId;
	        userService.getCurrentUser().then(function (currentUser) { $scope.user = currentUser; });

	        if ($routeParams.id == -1) {
	            $scope.node = {};
	            $scope.loaded = true;
	        }
	        else if (parentId !== undefined) {
	            //get a content id -> service
	            approvalResource.getPropertyById(parentId, $routeParams.id, $scope.user.locale).then(function (response) {
	                $scope.node = response.data;
	                $scope.loaded = true;

	                if ($scope.node.PropertyTypeAlias.indexOf('MultipleMediaPicker') !== -1) {
	                    entityResource.getById($scope.node.PreviousValue, "Media").then(function (media) {
	                        $scope.node.oldImage = media;
	                    });
	                    entityResource.getById($scope.node.CurrentValue, "Media").then(function (media) {
	                        $scope.node.currentImage = media;
	                    });
	                }
	                else {
	                    $scope.node.currentImage = undefined;
	                }
	            });
	        }
	    }

	    $scope.publish = function (node) {
	        approvalResource.publish(node.Id).then(function (response) {
	            $scope.node = response.data;
	            $scope.contentForm.$dirty = false;
	            navigationService.syncTree({ tree: 'approvalTree', path: [-1, -1], forceReload: true });
	            notificationsService.success("Success", node.Name + " has been published");
	        });
	    };

	    $scope.compare = function ($event) {
	        pickADiff($event, $scope);
	    };

	    $scope.callUpdate = function () {
	        if ($scope.node !== undefined && $scope.node.PreviousValue !== undefined) {
	            pickADiff($scope.node.PreviousValue, $scope.node.CurrentValue);
	        }
	    }
	});


function pickADiff(prev, curr, event) {

    if (event) {
        event.preventDefault();
    }

    $('.picadiff').picadiff({
        //leftContent: $('#previousValue').html(),
        //rightContent: $('#currentValue').html(),
        //leftContent: prev,
        //rightContent: curr,
        //lineLength: 1000,
    });;

    $(".picadiff").picadiff();
}

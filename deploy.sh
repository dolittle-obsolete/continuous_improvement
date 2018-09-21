#!/bin/bash
docker push dolittle/continuousimprovement
kubectl patch deployment continuousimprovement --namespace dolittle -p "{\"spec\":{\"template\":{\"metadata\":{\"labels\":{\"date\":\"`date +'%s'`\"}}}}}"

package iot.house.automation.microservices.mobile.api.controllers;

import iot.house.automation.microservices.mobile.domain.interfaces.MobileManagement;
import iot.house.automation.microservices.mobile.domain.models.mobile.Mobile;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/mobile")
public class MobileController {

    private final MobileManagement mobileManagement;

    public MobileController(MobileManagement mobileManagement) {
        this.mobileManagement = mobileManagement;
    }

    @RequestMapping(value = "/register", method = RequestMethod.POST)
    private ResponseEntity registerMobile(@RequestBody Mobile mobile) {

        if(!mobile.isValid()) {
            return new ResponseEntity(HttpStatus.BAD_REQUEST);
        }

        mobileManagement.register(mobile);
        return ResponseEntity.ok("Registered with success");
    }
}

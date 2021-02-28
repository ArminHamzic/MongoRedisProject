package at.htl.control

import at.htl.entity.SchoolSubject
import at.htl.entity.Schwammal
import at.htl.service.SchwammalService
import javax.ws.rs.*
import javax.ws.rs.core.MediaType
import javax.ws.rs.core.Response

@Path("/schwammal")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
class SchoolController(var schwammalService: SchwammalService) {

    @GET
    @Path("/all")
    fun getAllSchwammal(): List<Schwammal?>? {
        return schwammalService.getAllSchwammal()
    }

    @GET
    @Path("/{key}")
    fun getSchwammalByKey(@PathParam("key") key: String): Schwammal? {
        return schwammalService.getSchwammalByKey(key)
    }

    @POST
    fun addSchwammal(schwammal: Schwammal): Response? {
        schwammalService.addSchwammalToRedis(schwammal)
        return Response.status(201).build()
    }

    @PUT
    fun updateSchwammal(schwammal: Schwammal): Response? {
        schwammalService.updateSchwammal(schwammal)
        return Response.status(201).build()
    }

    @DELETE
    @Path("/{key}")
    fun deleteSchwammal(@PathParam("key") key: String): Response? {
        schwammalService.deleteSchwammal(key)
        return Response.noContent().build()
    }

    @POST
    @Path("/subject/{key}")
    fun addSubjectToSchwammal(@PathParam("key") key: String, subject: SchoolSubject): Response? {
        schwammalService.addSubjectToSchwammal(key, subject)
        return Response.status(201).build()
    }
}

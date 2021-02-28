package at.htl.service

import at.htl.entity.SchoolSubject
import at.htl.entity.Schwammal
import com.google.gson.Gson
import io.quarkus.redis.client.RedisClient
import io.quarkus.redis.client.RedisClientName
import io.vertx.redis.client.Response
import java.util.*
import java.util.stream.Stream
import javax.enterprise.context.ApplicationScoped
import javax.inject.Inject
import kotlin.streams.toList

@ApplicationScoped
class SchwammalService {

    @Inject
    var redisClient: RedisClient? = null

    var gson = Gson()

    fun getAllSchwammal(): List<Schwammal?>? {
//        return redisClient?.keys("*")?.map {
//            response -> gson.fromJson(response.toString(), Schwammal::class.java)
//        }
        var keys  = redisClient?.keys("*")?.map { response -> response.toString() }
        return keys?.map { r -> getSchwammalByKey(r) }
    }

    fun getSchwammalByKey(key: String): Schwammal? {
        return gson.fromJson(redisClient?.get(key).toString(), Schwammal::class.java)
    }

    fun deleteSchwammal(key: String) {
        redisClient?.del(listOf(key))
    }

    fun addSchwammalToRedis(schwammal: Schwammal) {
        if(schwammal.key.isEmpty()) {
            val key = UUID.randomUUID()
            schwammal.key = key.toString()
        }
        redisClient?.set(listOf(schwammal.key, gson.toJson(schwammal)))
    }

    fun updateSchwammal(schwammal: Schwammal) {
        deleteSchwammal(schwammal.key)
        addSchwammalToRedis(schwammal)
    }

    fun addSubjectToSchwammal(key: String, subject: SchoolSubject) {
        var schwammal = getSchwammalByKey(key)
        schwammal?.schoolSubjects?.add(subject)
        if (schwammal != null) {
            updateSchwammal(schwammal)
        }
    }
}

package at.htl

import io.quarkus.redis.client.RedisClient
import io.quarkus.runtime.StartupEvent
import javax.enterprise.context.ApplicationScoped

import javax.enterprise.event.Observes
import io.quarkus.redis.client.reactive.ReactiveRedisClient
import java.util.*
import javax.inject.Inject


@ApplicationScoped
class InitBean {

    @Inject
    var redisClient: RedisClient? = null

    @Inject
    var reactiveRedisClient: ReactiveRedisClient? = null

    fun onStartup(@Observes event: StartupEvent?) {
        set("first", 10)
        println(get("first"))
    }

    operator fun set(key: String?, value: Int) {
        redisClient!!.set(Arrays.asList(key, value.toString()))
    }

    operator fun get(key: String?): String? {
        return redisClient!![key].toString()
    }


}
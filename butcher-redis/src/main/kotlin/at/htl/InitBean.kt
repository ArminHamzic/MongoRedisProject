package at.htl

import io.quarkus.redis.client.RedisClient
import io.quarkus.redis.client.reactive.ReactiveRedisClient
import io.quarkus.runtime.QuarkusApplication
import io.quarkus.runtime.annotations.QuarkusMain
import java.util.*
import javax.inject.Inject

@QuarkusMain
class InitBean : QuarkusApplication {

    @Inject
    var redisClient: RedisClient? = null

    @Inject
    var reactiveRedisClient: ReactiveRedisClient? = null

    override fun run(vararg args: String?): Int {
        set("first", 10)
        println(get("first"))
        return 10;
    }

    operator fun set(key: String?, value: Int) {
        redisClient!!.set(Arrays.asList(key, value.toString()))
    }

    operator fun get(key: String?): String? {
        return redisClient!![key].toString()
    }

}